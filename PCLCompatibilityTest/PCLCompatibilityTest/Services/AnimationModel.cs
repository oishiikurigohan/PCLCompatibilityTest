using CocosSharp;
using Xamarin.Forms;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PCLCompatibilityTest.Services
{
    public class AnimationModel : ContentView
    {
        private static float firstPositionX;
        private static float firstPositionY;
        private static float distance;
        private static CCSprite hito;

        public AnimationModel()
        {
            var cocosSharpView = new CocosSharpView();
            cocosSharpView.ViewCreated += (sender, e) =>
            {
                var gameView = sender as CCGameView;
                var cocosSharpViewWidth = cocosSharpView.Width;
                var cocosSharpViewHeight = cocosSharpView.Height;
                gameView.DesignResolution = new CCSizeI((int)cocosSharpViewWidth, (int)cocosSharpViewHeight);
                var scene = new CCScene(gameView);
                var layer = new CCLayerColor(CCColor4B.White);

                hito = new CCSprite("hito.png", null);
                hito.Scale = 0.5f;

                firstPositionX = 0 - (hito.ContentSize.Width / 2);
                firstPositionY = (float)cocosSharpViewHeight / 2;
                hito.PositionX = firstPositionX;
                hito.PositionY = firstPositionY;
                distance = (float)cocosSharpViewWidth + hito.ContentSize.Width;

                layer.AddChild(hito);
                scene.AddLayer(layer);
                gameView.RunWithScene(scene);
            };
            Content = cocosSharpView;
        }

        public void Update(double sliderNewValue)
        {
            hito.StopAllActions();

            // 1回のCCMoveByアクションで進む距離を求める。0.15秒でスライダの値÷2進むことにする。
            var distanceUnit = (float)sliderNewValue / 2;

            // 何回のCCMoveByアクションで画面の端から端まで行けるかを算出。
            var actionCount = distance / distanceUnit;

            // Spriteの現在地情報から、終着点から最初の位置まで戻るアクションを入れる要素番号を取得する。
            var index = Convert.ToInt32(Math.Floor((distance - (hito.ContentSize.Width / 2) - hito.PositionX) / distanceUnit));

            // 端から端までCCMoveByで移動し、反対側の端に戻るシーケンスを組み立てる。
            var moveAction = new CCMoveBy(0.15f, new CCPoint(distanceUnit, 0));
            var backAction = new CCMoveTo(0, new CCPoint(firstPositionX, firstPositionY));
            List<CCFiniteTimeAction> actionList = Enumerable.Repeat<CCFiniteTimeAction>(moveAction, (int)actionCount).ToList();
            actionList.Insert(index,backAction);
            var sequence = new CCSequence(actionList.ToArray());

            // 上で組み立てたシーケンスを永遠に繰り返す。
            hito.RepeatForever(sequence);
        }
    }
}
