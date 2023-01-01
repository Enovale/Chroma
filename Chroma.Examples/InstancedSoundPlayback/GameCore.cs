﻿using System;
using System.IO;
using Chroma;
using Chroma.Audio;
using Chroma.ContentManagement;
using Chroma.ContentManagement.FileSystem;
using Chroma.ContentManagement.FileSystem.ContentProviders;
using Chroma.Graphics;
using Chroma.Input;

namespace InstancedSoundPlayback
{
    public class GameCore : Game
    {
        private InstancedSound _instancedSound;
        
        internal static AudioOutput AudioOutput { get; set; }
        
        public GameCore() 
            : base(new(false, false))
        {
            AudioOutput = Audio.Output;
        }

        protected override IContentProvider InitializeContentPipeline()
        {
            IContentProvider pipeline; 
            
            if (OperatingSystem.IsAndroid())
                pipeline = base.InitializeContentPipeline();
            else
                pipeline = new FileSystemContentProvider(
                    Path.Combine(FileSystemUtils.BaseDirectory, "../../../../_common")
                );

            pipeline.RegisterImporter<InstancedSound>((path, _) => new InstancedSound(path));
            
            return pipeline;
        }
        
        protected override void LoadContent()
        {
            _instancedSound = Content.Load<InstancedSound>("Sounds/doomsg.wav");
        }

        protected override void Update(float delta)
        {
            InstancedSound.Update();
        }

        protected override void Draw(RenderContext context)
        {
            context.DrawString(
                "Press and/or hold any key to play a new instance of the same sound.\n" +
                $"{InstancedSound.LiveInstanceCount} live instance(s).",
                8, 8
            );
        }

        protected override void KeyPressed(KeyEventArgs e)
        {
            _instancedSound.PlayInstance();
        }
    }
}