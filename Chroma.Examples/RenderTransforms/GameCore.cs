using System;
using System.IO;
using System.Numerics;
using Chroma;
using Chroma.ContentManagement.FileSystem;
using Chroma.Graphics;

namespace RenderTransforms
{
    public class GameCore : Game
    {
        private Texture _burger;
        private RenderTarget _rt;
        
        public GameCore() : base(false)
        {
            Content = new FileSystemContentProvider(
                Path.Combine(AppContext.BaseDirectory, "../../../../_common")
            );
        }

        protected override void LoadContent()
        {
            _burger = Content.Load<Texture>("Textures/burg.png");
            _rt = new RenderTarget(_burger.Width * 2, _burger.Height * 2);

            Transform.SetMatrixMode(MatrixMode.Projection, Window);
            Transform.LoadIdentity();
            Transform.Orthographic(
                0,
                0,
                Window.Size.Width,
                Window.Size.Height,
                1,
                -1
            );
        }

        private float angle = 0;
        protected override void Update(float delta)
        {
            angle += 2f;
        }

        protected override void Draw(RenderContext context)
        {          
            context.RenderTo(_rt, () =>
            {
                context.Clear(Color.Aqua);

                Transform.SetMatrixMode(MatrixMode.Model, _rt);
                Transform.Push();
                Transform.LoadIdentity();
                
                Transform.Translate(_burger.Center);
                Transform.Rotate(angle, new Vector3(0, 0, 1f));
                Transform.Translate(-_burger.Center);
                
                context.DrawTexture(_burger, Vector2.Zero, Vector2.One, Vector2.Zero, 0);
                Transform.Pop();
            });
            
            context.DrawTexture(_rt, Window.Center, Vector2.One, _rt.Center, 0);
        }
    }
}