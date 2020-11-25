﻿using System;
using System.IO;
using Chroma.Audio.Filters;
using Chroma.Diagnostics.Logging;
using Chroma.Natives.SoLoud;

namespace Chroma.Audio.Sources
{
    public class Music : AudioSource
    {
        private readonly Log _log = LogManager.GetForCurrentAssembly();

        public override double LoopingPoint
        {
            get => SoLoud.WavStream_getLoopPoint(Handle);
            set => SoLoud.WavStream_setLoopPoint(Handle, value);
        }

        public override bool SupportsLength => true;
        public override double Length => SoLoud.WavStream_getLength(Handle);
        
        public Music(string filePath)
            : base(SoLoud.WavStream_create())
        {
            ValidateHandle();

            var error = SoLoud.WavStream_load(Handle, filePath);
            if (error > 0)
            {
                _log.Error(
                    $"Failed to load music from file: " +
                    $"{SoLoud.Soloud_getErrorString(AudioManager.Instance.Handle, error)}"
                );

                Dispose();
                return;
            }
            
            InitializeState();
        }

        public Music(Stream stream)
            : base(SoLoud.WavStream_create())
        {
            ValidateHandle();

            int error;
            
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                var data = ms.ToArray();

                unsafe
                {
                    fixed (byte* b = &data[0])
                    {
                        error = SoLoud.WavStream_loadMemEx(
                            Handle,
                            new IntPtr(b),
                            (uint)data.Length,
                            true,
                            true
                        );
                    }
                }
            }
            
            if (error > 0)
            {
                _log.Error(
                    $"Failed to load music from stream: " +
                    $"{SoLoud.Soloud_getErrorString(AudioManager.Instance.Handle, error)}"
                );

                Dispose();
            }
        }

        protected override void SetInaudibleBehavior(bool mustTick, bool killAfterGoingSilent)
            => SoLoud.WavStream_setInaudibleBehavior(Handle, mustTick, killAfterGoingSilent);

        protected override void SetLooping(bool looping)
            => SoLoud.WavStream_setLooping(Handle, looping);

        protected override void ApplyFilter(int slot, AudioFilter filter)
            => SoLoud.WavStream_setFilter(Handle, (uint)slot, filter.Handle);

        protected override void ClearFilter(int slot)
            => SoLoud.WavStream_setFilter(Handle, (uint)slot, IntPtr.Zero);
        
        protected override void SetVolume(float volume)
            => SoLoud.WavStream_setVolume(Handle, volume);

        protected override void FreeNativeResources()
        {
            if (Handle != IntPtr.Zero)
            {
                SoLoud.WavStream_stop(Handle);
                SoLoud.WavStream_destroy(Handle);

                DestroyHandle();
            }
        }
    }
}