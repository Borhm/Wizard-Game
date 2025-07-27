using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace WizardGame
{
    class Circles
    {
        public int x, y,w,h;
        public int x2, y2,w2,h2;
    }
    class AdvImageHero
    {
        public Rectangle src;
        public Rectangle dst;
        public List<Bitmap> standing = new List<Bitmap>();
        public List<Bitmap> standingBack = new List<Bitmap>();
        public List<Bitmap> Walking = new List<Bitmap>();
        public List<Bitmap> WalkingBack = new List<Bitmap>();
        public List<Bitmap> RunRight = new List<Bitmap>();
        public List<Bitmap> RunLeft = new List<Bitmap>();
        public List<Bitmap> SwordOne = new List<Bitmap>();
        public List<Bitmap> SwordOneLeft = new List<Bitmap>();
        public List<Bitmap> SwordTwo = new List<Bitmap>();
        public List<Bitmap> SwordTwoLeft = new List<Bitmap>();
        public List<Bitmap> Jump = new List<Bitmap>();
        public List<Bitmap> JumpBack = new List<Bitmap>();
        public List<Bitmap> Dead = new List<Bitmap>();
        public List<Bitmap> DeadLeft = new List<Bitmap>();
        public List<Bitmap> Hurt = new List<Bitmap>();
        public List<Bitmap> HurtLeft = new List<Bitmap>();
        public List<Bitmap> FireBall = new List<Bitmap>();
        public List<Bitmap> FireBallLeft = new List<Bitmap>();
        public List<Bitmap> Charge = new List<Bitmap>();
        public List<Bitmap> Flame = new List<Bitmap>();
        public List<Bitmap> FlameLeft = new List<Bitmap>();
        public List<Bitmap> Health = new List<Bitmap>();

        public int dir = 1;
    
        public int IfStanding = 0;
        public int IfWalking = 0;
        public int IfWalkingBack = 0;
        public int IfRunRight = 0;
        public int IfSwordOne = 0;
        public int IfSwordTwo = 0;
        public int IfJump = 0;
        public int IfDead = 0;
        public int IfHurt = 0;
        public int IfFireBall = 0;
        public int IfCharge = 0;
        public int IfFlame = 0;
        public int IfHealth = 0;
    }
    class AdvImgBackGround
    {
        public Rectangle src;
        public Rectangle dst;
        public Bitmap img;
        public int IfStanding = 0;
    }
    class FireBall
    {
        public Rectangle dst;
        public Rectangle chargedst;
        public int direction;
        public int frameindex = 0;
        public int stage = 0; 
    }


    public partial class Form1 : Form
    {
        string typedText = ""; 
        string winningWord = "WIN"; 

        /// <summary>
        /// /
        /// </summary>
        int fireballDir = 1;
        //int savedDir = 1;

        //Ginie
        Rectangle genie_dst;

        int FlagCasting = 0; 
        int CastingCounter = 0;
        int MaxCastingFrames = 8;
        int genie_offset = 0;
        int genie_direction = 1;
        List<Bitmap> genie_flight = new List<Bitmap>();
        List<Bitmap> genie_attack = new List<Bitmap>();
        List<Bitmap> genie_majicAttack = new List<Bitmap>();
        List<Bitmap> genie_Death = new List<Bitmap>();
        int genie_idle_index = 0;
        int genie_attack_Mi = 0;
        int genie_attack_index = 0;
        int genie_mode = 0;
        int genie_timerct = 0;
        int genieFlight_duration = 20;
        int magic_xOffset = 0;
        int magic_xSpeed = 40;
        int genieAtt_repeatCounter = 0;
        bool genie_dead = false;
        int geniedeath_delayCt = 0;
        int geniedeath_delay = 3;
        int genie_callct = 0;
        int max_genie = 3;
        bool showGenie = false;
        int timerct=0;
        //
        List<FireBall> Lfireballs= new List<FireBall>();
        //
        /// ////////////////////////////////
        /// 
       

        float cameraSpeed = 0.1f; // كل ما الرقم قل، الحركة تبقى أنعم
        int groundY = 125; // مكان الأرض اللي واقف عليها البطل
        int speed = 15;
        float velocityY = 0;
        float gravity = 8f;//6;
        float jumpStrength = -60f;
        static int PrepFrameCounter = 0;
        int jumpDirection = 0; // 0 = ثابت، -1 = شمال، 1 = يمين
        int jumpCount = 0;
        int maxJumps = 2;
        //
        //
        //
        Rectangle chargeDst;
        List<Circles> leyes = new List<Circles>();
        List<Circles> lPupil = new List<Circles>();
        int FlagTreeEye = 0;
        int FlagTreePupil = 0;
        int FlagFlame = 0;
        int FlagCharge = 0;
        int FlagFireBall= 0;
        int FlagHurt = 0;
        int FlagDead = 0;
        int FlagJump =0;
        int FlagSwordTwo = 0;
        int FlagSwordOne = 0;
        int FlagRun = 0;
        int cameraX = 1;
        int heroWorldX = 100; // موقع البطل الحقيقي في العالم&&لو عايز اغير مكانه من هنا!!
        int flagstanding = 1;
        List<AdvImageHero> Lhero = new List<AdvImageHero>();
        Rectangle Heart_dst;
        Rectangle Heart_src;
        List<AdvImgBackGround> LBackGrounds = new List<AdvImgBackGround>();
        Bitmap OFF;
        Timer timer = new Timer();
        public Form1()
        {
            this.KeyUp += Form1_KeyUp;
            this.KeyDown += Form1_KeyDown;
            this.Load += Form1_Load1;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.WindowState = FormWindowState.Maximized;
            timer.Start();
            timer.Interval = 50;//100
            timer.Tick += Timer_Tick;
            //this.KeyPreview = true;

        }//ChangeTimer
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            char keyChar = (char)e.KeyCode;
            CheatWin(keyChar);

            bool isJumping = (FlagJump == 1 || velocityY != 0);

            if (e.KeyCode == Keys.Space && jumpCount < maxJumps)
            {
                Jump();
                return;
            }
            if (e.KeyCode == Keys.P)
            {
                HeroHealth();
            }

            if (isJumping&&jumpCount<=2)
            {
                return;
            }

            if (e.KeyCode == Keys.G && genie_callct < max_genie)
            {
                ActivateGenie();
            }
            else if (e.KeyCode == Keys.Right)
            {
               ToRight();
            }
            else if (e.KeyCode == Keys.Left)
            {
                ToLeft();
            }
            else if (e.KeyCode == Keys.R)
            {
                ToggleRun();
            }
            else if (e.KeyCode == Keys.A)
            {
                SwordAttackOne();
            }
            else if (e.KeyCode == Keys.S)
            {
                SwordAttackTwo();
            }
            else if (e.KeyCode == Keys.D)
            {
                Dead();
            }
            else if (e.KeyCode == Keys.H)
            {
                Hurt();
            }
            else if (e.KeyCode == Keys.Q)
            {
                FireBall();
            }
            else if (e.KeyCode == Keys.W)
            {
                Flame();
            }
            else if (e.KeyCode == Keys.M) 
            {

                SpawnFireBall();
            }




            UpdateCamera();
            //DrawDubbleBuffer();//The Proplem Was Here
        }//Remove Db
        void HeroHealth()
        {
            
            Lhero[0].IfHealth++;
            if (Lhero[0].IfHealth >= Lhero[0].Health.Count)
            {
                Dead();
                Lhero[0].IfHealth = Lhero[0].Health.Count-1;
                //Lhero[0].IfHealth = 0;

            }
        }
        void SpawnFireBall()
        {
            fireballDir = Lhero[0].dir;

            FireBall fb = new FireBall();
            fb.direction = Lhero[0].dir;
            fb.dst = new Rectangle(
                Lhero[0].dst.X,
                Lhero[0].dst.Y,
                Lhero[0].dst.Width,
                Lhero[0].dst.Height
            );

            if (fb.direction == 1)
                fb.chargedst = new Rectangle(fb.dst.X + fb.dst.Width - 350, fb.dst.Y + 200, fb.dst.Width, fb.dst.Height);
            else
                fb.chargedst = new Rectangle(fb.dst.X - fb.dst.Width + 350, fb.dst.Y + 200, fb.dst.Width, fb.dst.Height);

            Lfireballs.Add(fb);

            FlagCasting = 1;
            CastingCounter = 0;
        }

        void CheatWin(char keyChar)
        {


            if (char.IsLetter(keyChar))
            {

                typedText += keyChar;


                if (typedText.Length > winningWord.Length)
                {
                    typedText = typedText.Substring(typedText.Length - winningWord.Length);
                }


                if (typedText == winningWord)
                {
                    MessageBox.Show("You Win! 🎉");
                    typedText = "";
                }
            }
        }
        void ToRight()
        {
            if (FlagRun != 1)
            {

                flagstanding = 2; // ✅ بدل IfStanding
                heroWorldX += speed;
                Lhero[0].IfWalking++;
                Lhero[0].dir = 1;
                if (Lhero[0].IfWalking >= Lhero[0].Walking.Count)
                {
                    Lhero[0].IfWalking = 0;
                }
            }
            else
            {
                heroWorldX += speed + 5;//10
                Lhero[0].dir = 1;
                Lhero[0].IfRunRight++;
                if (Lhero[0].IfRunRight >= Lhero[0].RunRight.Count)
                {
                    Lhero[0].IfRunRight = 0;
                }
            }
        }//ChangeSpeed
        void ActivateGenie()
        {
            genie_callct++;
            showGenie = true;
            genie_dead = false;
            genie_mode = 0;
            genie_offset = 0;
            genie_direction = 1;
            genie_idle_index = 0;
            genie_attack_index = 0;
            genie_attack_Mi = 0;
            genieAtt_repeatCounter = 0;
            magic_xOffset = 0;
            genie_timerct = 0;
            geniedeath_delayCt = 0;
        }

        void ToLeft()
        {
            flagstanding = 2;
            Lhero[0].dir = -1;

            if (FlagRun != 1)
            {
                heroWorldX -= speed;
                if (heroWorldX < 0) heroWorldX = 0;

                Lhero[0].IfWalkingBack++;
                if (Lhero[0].IfWalkingBack >= Lhero[0].WalkingBack.Count)
                {
                    Lhero[0].IfWalkingBack = 0;
                }
            }
            else
            {
                heroWorldX -= speed + 10;
                if (heroWorldX < 0) heroWorldX = 0;

                Lhero[0].IfRunRight++;
                if (Lhero[0].IfRunRight >= Lhero[0].RunLeft.Count)
                {
                    Lhero[0].IfRunRight = 0;
                }
            }
        }

        void ToggleRun()
        {
            if (FlagRun == 1)
                FlagRun = 0;
            else
                FlagRun = 1;
        }

        void SwordAttackOne()
        {
            FlagSwordOne = 1;
        }

        void SwordAttackTwo()
        {
            FlagSwordTwo = 1;
        }

        void Jump()
        {
            FlagJump = 1;
            velocityY = jumpStrength;
            jumpCount++;

            if (Lhero[0].dir == 1)
                jumpDirection = 1;
            else if (Lhero[0].dir == -1)
                jumpDirection = -1;
            else
                jumpDirection = 0;
        }

        void Dead()
        {
            FlagDead = 1;
        }

        void Hurt()
        {
            FlagHurt = 1;
        }

        void FireBall()
        {
            FlagFireBall = 1;
            fireballDir = Lhero[0].dir;
        }

        void Flame()
        {
            FlagFlame = 1;
        }


        void UpdateCamera()
        {
            int targetCameraX = heroWorldX - (this.Width / 2);

            if (targetCameraX < 0)
                targetCameraX = 0;

            // نعمل حركة تدريجية ناحية المكان المستهدف
            cameraX += (int)((targetCameraX - cameraX) * cameraSpeed);

            Lhero[0].dst.X = heroWorldX - cameraX - 600;
        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            flagstanding = 1; // ✅ البطل واقف
            FlagRun = 0; // ✅ البطل واقف
        }
        //Creation
        void CreateEyesOfTree()
        {
            int BrownCircleX = 730;
            int WhiteCircleX = 732;
            int clr = 810;
            int clr2 = 812;
            int space = 0;
            
            int cy = 235;
            int cywhite = 237;
            for(int i=0; i<4;i++ )
            {
                Circles c= new Circles();
                c.x = BrownCircleX+space;
                c.y = cy;
                c.w = 15;
                c.h = 15;
                leyes.Add( c );
                Circles c2 = new Circles();
                c2.x = clr+space;
                c2.y = cy;
                c2.w = 15;
                c2.h = 15;
                leyes.Add(c2);
                //
                Circles p = new Circles();
                p.x = WhiteCircleX + space;
                p.y = cywhite;
                p.w = 10;
                p.h = 10;
                p.x2 = clr2+space;
                p.y2 = cywhite;
                p.w2 = 10;
                p.h2 = 10;

                lPupil.Add(p);
                //Circles p2 = new Circles();
                //p2.x = clr2 + space;
                //p2.y = cywhite;
                //p2.w = 10;
                //p2.h = 10;
                //lPupil.Add(p2);

                space += 1550;
            }
        }
        void createGenie()
        {

            for (int i = 1; i < 4; i++)
            {
                Bitmap ptrav = new Bitmap("Genie\\Flight\\Flight" + i + ".png");
                ptrav.MakeTransparent();
                genie_flight.Add(ptrav);
            }
            for (int i = 1; i <= 4; i++)
            {
                Bitmap ptrav = new Bitmap("Genie\\Attack\\Attack" + i + ".png");
                ptrav.MakeTransparent();
                genie_attack.Add(ptrav);
            }
            for (int i = 1; i < 13; i++)
            {
                Bitmap ptrav = new Bitmap("Genie\\MagicAttack\\Magic_Attack" + i + ".png");
                ptrav.MakeTransparent();
                genie_majicAttack.Add(ptrav);
            }
            for (int i = 3; i < 6; i++)
            {
                Bitmap ptrav = new Bitmap("Genie\\Death\\Death" + i + ".png");
                ptrav.MakeTransparent();
                genie_Death.Add(ptrav);
            }
            Bitmap ptt = new Bitmap("Genie\\MagicAttack\\Magic_Attack1.png");//من اول هنا يا بولبولطييييييييييييييي
            genie_majicAttack.Insert(0, ptt);
            genie_majicAttack.Insert(0, ptt);
            genie_majicAttack.Insert(0, ptt);
            genie_majicAttack.Insert(0, ptt);
            genie_majicAttack.Insert(0, ptt);
            genie_majicAttack.Insert(0, ptt);
            genie_majicAttack.Insert(0, ptt);
            genie_majicAttack.Insert(0, ptt);
            genie_majicAttack.Insert(0, ptt);
            genie_majicAttack.Insert(0, ptt);
            genie_majicAttack.Insert(0, ptt);
            genie_majicAttack.Insert(0, ptt);
            genie_majicAttack.Insert(0, ptt);
            genie_majicAttack.Insert(0, ptt);
            genie_majicAttack.Insert(0, ptt);
        }
        void CreateBackGrounds()
        {
            for (int i = 1; i < 5; i++)
            {
                AdvImgBackGround pnn = new AdvImgBackGround();
                pnn.img = new Bitmap("Background\\Forest\\0" + i + ".png");
                pnn.src = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
                int index = i - 3;
                pnn.dst = new Rectangle(this.Width * index, 0, this.Width, this.Height);
                pnn.img = new Bitmap(pnn.img, new Size(this.Width, this.Height));

                LBackGrounds.Add(pnn);
            }
            for (int i = 0; i < 3; i++)
            {
                AdvImgBackGround pnn = new AdvImgBackGround();
                pnn.img = new Bitmap("Background\\SkullCity\\" + i + ".png");
                pnn.src = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
                int index = i - 3;
                pnn.dst = new Rectangle(this.Width * index, 0, this.Width, this.Height);
                pnn.img = new Bitmap(pnn.img, new Size(this.Width, this.Height));

                LBackGrounds.Add(pnn);
            }
            for (int i = 6; i < 10; i++)
            {

                AdvImgBackGround pnn = new AdvImgBackGround();
                pnn.img = new Bitmap("Background\\Castle\\" + i + ".png");
                pnn.src = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
                int index = i - 3;
                pnn.dst = new Rectangle(this.Width * index, 0, this.Width, this.Height);
                pnn.img = new Bitmap(pnn.img, new Size(this.Width, this.Height));

                LBackGrounds.Add(pnn);
            }
        }
        void Createhero()
        {
            AdvImageHero pnn = new AdvImageHero();
            for (int i = 0; i < 7; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Standing\\"+i+".png");
                trav.MakeTransparent();
                pnn.standing.Add(trav);
            }
            for (int i = 0; i < 7; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Standing\\0" + i + ".png");
                trav.MakeTransparent();
                pnn.standingBack.Add(trav);
            }
            for (int i = 0; i < 6; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\walking\\"+i+".png");
                trav.MakeTransparent();
                pnn.Walking.Add(trav);
            }
            for (int i = 0; i < 6; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\walking\\0" + i + ".png");
                trav.MakeTransparent();
                pnn.WalkingBack.Add(trav);
            }
            for (int i = 0; i < 8; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Run\\0" + i + "_Run.png");
                trav.MakeTransparent();
                pnn.RunRight.Add(trav);
            }
            for (int i = 0; i < 8; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Run\\" + i + "_Run.png");
                trav.MakeTransparent();
                pnn.RunLeft.Add(trav);
            }
            for (int i = 0; i < 4; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Sword1\\0"+i+"_Attack_1.png");
                trav.MakeTransparent();
                pnn.SwordOne.Add(trav);
            }
            for (int i = 0; i < 4; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Sword1\\" + i + "_Attack_1.png");
                trav.MakeTransparent();
                pnn.SwordOneLeft.Add(trav);
            }
            for (int i = 0; i < 4; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Sword2\\0" + i + "_Attack_2.png");
                trav.MakeTransparent();
                pnn.SwordTwo.Add(trav);
            }
            for (int i = 0; i < 4; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Sword2\\" + i + "_Attack_2.png");
                trav.MakeTransparent();
                pnn.SwordTwoLeft.Add(trav);
            }
            for (int i = 0; i < 9; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Jump\\0"+i+"_Jump.png");
                trav.MakeTransparent();
                pnn.Jump.Add(trav);
            }
            for (int i = 0; i < 9; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Jump\\" + i + "_Jump.png");
                trav.MakeTransparent();
                pnn.JumpBack.Add(trav);
            }
            for (int i = 0; i < 6; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Dead\\0"+i+"_Dead.png");
                trav.MakeTransparent();
                pnn.Dead.Add(trav);
            }
            for (int i = 0; i < 6; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Dead\\" + i + "_Dead.png");
                trav.MakeTransparent();
                pnn.DeadLeft.Add(trav);
            }
            for (int i = 0; i < 3; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Hurt\\0"+i+"_Hurt.png");
                trav.MakeTransparent();
                pnn.Hurt.Add(trav);
            }
            for (int i = 1; i < 8; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Health\\"+i+"h-removebg-preview.png");//heart
                trav.MakeTransparent();
                pnn.Health.Add(trav);
            }

            for (int i = 0; i < 3; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Hurt\\" + i + "_Hurt.png");
                trav.MakeTransparent();
                pnn.HurtLeft.Add(trav);
            }
            for (int i = 0; i < 8; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\FireBall\\0"+i+"_Fireball.png");
                trav.MakeTransparent();
                pnn.FireBall.Add(trav);
            }
            for (int i = 0; i < 8; i++)
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\FireBall\\" + i + "_Fireball.png");
                trav.MakeTransparent();
                pnn.FireBallLeft.Add(trav);
            }
            for (int i = 0; i < 10; i++)//12
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Charge\\0"+i+"_Charge.png");
                trav.MakeTransparent();
                pnn.Charge.Add(trav);
            }
            pnn.Charge.Insert(1,new Bitmap( "Wizard\\Boy\\Charge\\00_Charge.png"));
            pnn.Charge.Insert(1,new Bitmap( "Wizard\\Boy\\Charge\\00_Charge.png"));
            pnn.Charge.Insert(1,new Bitmap( "Wizard\\Boy\\Charge\\00_Charge.png"));
            pnn.Charge.Insert(1,new Bitmap( "Wizard\\Boy\\Charge\\00_Charge.png"));
            pnn.Charge.Insert(1,new Bitmap( "Wizard\\Boy\\Charge\\00_Charge.png"));
            pnn.Charge.Insert(1,new Bitmap( "Wizard\\Boy\\Charge\\00_Charge.png"));
            pnn.Charge.Insert(1,new Bitmap( "Wizard\\Boy\\Charge\\00_Charge.png"));




            Bitmap trav2 = new Bitmap("Wizard\\Boy\\Charge\\10_Charge.png");
            trav2.MakeTransparent();
            pnn.Charge.Add(trav2);
            trav2 = new Bitmap("Wizard\\Boy\\Charge\\11_Charge.png");
            trav2.MakeTransparent();
            pnn.Charge.Add(trav2);
            for (int i = 0; i < 10; i++)//14
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Flame\\0" + i + "_Flame_jet.png");
                trav.MakeTransparent();
                pnn.Flame.Add(trav);
            }

            for (int i = 0; i < 10; i++)//14
            {
                Bitmap trav = new Bitmap("Wizard\\Boy\\Flame\\" + i + "_Flame_jet.png");
                trav.MakeTransparent();
                pnn.FlameLeft.Add(trav);
            }
            trav2 = new Bitmap("Wizard\\Boy\\Flame\\11_Flame_jet.png");
            trav2.MakeTransparent();
            pnn.Flame.Add(trav2);
            trav2 = new Bitmap("Wizard\\Boy\\Flame\\12_Flame_jet.png");
            trav2.MakeTransparent();
            pnn.Flame.Add(trav2);
            trav2 = new Bitmap("Wizard\\Boy\\Flame\\13_Flame_jet.png");
            trav2.MakeTransparent();
            pnn.Flame.Add(trav2);


            trav2 = new Bitmap("Wizard\\Boy\\Flame\\010_Flame_jet.png");
            trav2.MakeTransparent();
            pnn.FlameLeft.Add(trav2);
            trav2 = new Bitmap("Wizard\\Boy\\Flame\\011_Flame_jet.png");
            trav2.MakeTransparent();
            pnn.FlameLeft.Add(trav2);
            trav2 = new Bitmap("Wizard\\Boy\\Flame\\012_Flame_jet.png");
            trav2.MakeTransparent();
            pnn.FlameLeft.Add(trav2);
            trav2 = new Bitmap("Wizard\\Boy\\Flame\\013_Flame_jet.png");
            trav2.MakeTransparent();
            pnn.FlameLeft.Add(trav2);


            //hero
            pnn.src = new Rectangle(0, 0, pnn.standing[0].Width, pnn.standing[0].Height);
            pnn.dst = new Rectangle(-265, 125, pnn.standing[0].Width * 4, pnn.standing[0].Height * 4);
            Lhero.Add(pnn);
            Heart_dst=new Rectangle(-25,-30, Lhero[0].Health[0].Width/2, Lhero[0].Health[0].Height/2);
            Heart_src=new Rectangle(0, 0, Lhero[0].Health[0].Width, Lhero[0].Health[0].Height);
        }

        //Draw...
        void drawcircles(Graphics g2)
        {
            
            if (FlagTreeEye==1)
            {
                for (int i = 0; i < leyes.Count; i++)
                {
                    g2.FillEllipse(Brushes.SaddleBrown, leyes[i].x - cameraX, leyes[i].y, leyes[i].w, leyes[i].h);
                }
                
            }
        }
        void DrawFireBalls(Graphics g2)
        {
            for (int i = 0; i < Lfireballs.Count; i++)
            {
                FireBall fb = Lfireballs[i];

                if (fb.stage == 0 && fb.frameindex < Lhero[0].FireBall.Count)
                {
                    List<Bitmap> frames = fb.direction == 1 ? Lhero[0].FireBall : Lhero[0].FireBallLeft;
                    g2.DrawImage(frames[fb.frameindex], fb.dst, Lhero[0].src, GraphicsUnit.Pixel);
                }
                else if (fb.stage == 1 && fb.frameindex < Lhero[0].Charge.Count)
                {
                    g2.DrawImage(Lhero[0].Charge[fb.frameindex], fb.chargedst, Lhero[0].src, GraphicsUnit.Pixel);
                }
            }
        }

        void drawpupils(Graphics g2)//laser------->FlagJump!=1
        {
            Pen p = new Pen(Color.DarkGreen, 5);
            if (FlagTreePupil == 1)
            {
                int heroCenterX = Lhero[0].dst.X + Lhero[0].dst.Width / 2;
                int heroCenterY = Lhero[0].dst.Y + Lhero[0].dst.Height / 2;
                int heroWorldX = heroCenterX + cameraX;

                for (int i = 0; i < lPupil.Count; i++)
                {
                    g2.FillEllipse(Brushes.RosyBrown, lPupil[i].x - cameraX, lPupil[i].y, lPupil[i].w, lPupil[i].h);
                    g2.FillEllipse(Brushes.RosyBrown, lPupil[i].x2 - cameraX, lPupil[i].y2, lPupil[i].w2, lPupil[i].h2);

                    int diff = heroWorldX - lPupil[i].x;
                    if (diff < 0) diff = 0 - diff;

                    if (diff <= 700)
                    {
                        int pupilCenterX = lPupil[i].x - cameraX + lPupil[i].w / 2;
                        int pupilCenterY = lPupil[i].y + lPupil[i].h / 2;
                        if (FlagJump != 1)//FlagJump!=1
                        {
                            g2.DrawLine(p, pupilCenterX, pupilCenterY, heroCenterX, heroCenterY);
                            g2.DrawLine(p, pupilCenterX + 80, pupilCenterY, heroCenterX, heroCenterY);
                            FlagHurt = 1;//

                            HeroHealth();
                        }

                    }
                }
            }
        }
        void DrawScene(Graphics g2)
        {
            g2.Clear(Color.Black);
            //g2.FillRectangle(Brushes.Red, 2000 - cameraX, 0, 100, 50);
            Scrolling(g2);
            DrawHero(g2);
            DrawHeart(g2);
            drawcircles( g2);
            drawpupils(g2);
            if (FlagDead == 0)
            {
                DrawFireBalls(g2);
            }



            this.Text = "HeroDistination="+Lhero[0].dst.X + "  ,HeroWorldX=" + heroWorldX+"  ,Timer="+timerct;
        }
        void DrawHeart(Graphics g2)
        {
            
            //g2.FillRectangle(Brushes.Black,0,0,4000,80);
            g2.DrawImage(Lhero[0].Health[Lhero[0].IfHealth], Heart_dst,Heart_src, GraphicsUnit.Pixel);
            //string s = "Health";

            // تحديد خط بسيط (مثلاً Arial بحجم 12)
            //Font f = new Font("Comic Sans MS", 48, FontStyle.Bold);
            Font f = new Font("Impact", 48, FontStyle.Bold);



            // تحديد فرشة الكتابة (لون أسود مثلاً)
            //Brush b = Brushes.DarkRed;

            // نحدد مكان الكتابة (مثلاً نفس مكان الصورة أو فوقها شوية)
            //float x = Heart_dst.X+Lhero[0].Health[0].Width-200;
            //float y = Heart_dst.Y ;

            // رسم النص
            //g2.DrawString(s, f, b, x, y);

            
        }
        void DrawHero(Graphics g2)
        {
            if (Lfireballs.Count > 0 && Lfireballs[0].stage == 0)
                return;

            if (FlagSwordOne == 1)
            {
                if (Lhero[0].dir == 1)
                    g2.DrawImage(Lhero[0].SwordOne[Lhero[0].IfSwordOne], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                else
                    g2.DrawImage(Lhero[0].SwordOneLeft[Lhero[0].IfSwordOne], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
            }

            else if (FlagDead == 1)
            {
                //flagstanding = 0;
                
                if (Lhero[0].dir == 1)
                    g2.DrawImage(Lhero[0].Dead[Lhero[0].IfDead], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                else
                    g2.DrawImage(Lhero[0].DeadLeft[Lhero[0].IfDead], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
            }


            else if (FlagCharge == 1)
            {
                if (fireballDir == 1)
                    g2.DrawImage(Lhero[0].standing[Lhero[0].IfStanding], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                else
                    g2.DrawImage(Lhero[0].standingBack[Lhero[0].IfStanding], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);

                g2.DrawImage(Lhero[0].Charge[Lhero[0].IfCharge], chargeDst, Lhero[0].src, GraphicsUnit.Pixel);
            }

            else if (FlagFireBall == 1)
            {
                if (fireballDir == 1)
                    g2.DrawImage(Lhero[0].FireBall[Lhero[0].IfFireBall], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                else
                    g2.DrawImage(Lhero[0].FireBallLeft[Lhero[0].IfFireBall], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
            }
            //else if (FlagCharge == 1)//Bullettttttttttttttttttttttttttttttttttttttt
            //{




            //    //g2.DrawImage(Lhero[0].Charge[Lhero[0].IfCharge], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
            //    g2.DrawImage(Lhero[0].standing[Lhero[0].IfStanding], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
            //    g2.DrawImage(Lhero[0].Charge[Lhero[0].IfCharge], chargedst, Lhero[0].src, GraphicsUnit.Pixel);

            //}

            else if (FlagHurt == 1)
            {
                if (Lhero[0].dir == 1)
                    g2.DrawImage(Lhero[0].Hurt[Lhero[0].IfHurt], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                else
                    g2.DrawImage(Lhero[0].HurtLeft[Lhero[0].IfHurt], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
            }

            else if (FlagFlame == 1)
            {
                if (Lhero[0].dir == 1)
                    g2.DrawImage(Lhero[0].Flame[Lhero[0].IfFlame], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                else
                    g2.DrawImage(Lhero[0].FlameLeft[Lhero[0].IfFlame], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
            }

            else if (FlagJump == 1)
            {
                if (Lhero[0].dir == 1)
                {
                    g2.DrawImage(Lhero[0].Jump[Lhero[0].IfJump], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                }
                else
                {
                    g2.DrawImage(Lhero[0].JumpBack[Lhero[0].IfJump], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                }
            }

            else if (FlagSwordTwo == 1)
            {
                if (Lhero[0].dir == 1)
                    g2.DrawImage(Lhero[0].SwordTwo[Lhero[0].IfSwordTwo], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                else
                    g2.DrawImage(Lhero[0].SwordTwoLeft[Lhero[0].IfSwordTwo], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
            }

            else if (FlagRun == 1)
            {
                if (Lhero[0].dir == 1)
                {
                    g2.DrawImage(Lhero[0].RunRight[Lhero[0].IfRunRight], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                }
                else
                {
                    g2.DrawImage(Lhero[0].RunLeft[Lhero[0].IfRunRight], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                }
            }

            else if (flagstanding == 1 && FlagCasting == 0)
            {
                if (Lhero[0].dir == 1)
                {
                    g2.DrawImage(Lhero[0].standing[Lhero[0].IfStanding], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                
                }
                else
                {
                    g2.DrawImage(Lhero[0].standingBack[Lhero[0].IfStanding], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                }

                   
            }



            else if (flagstanding == 2)
            {
                if (Lhero[0].dir == 1)
                {
                    g2.DrawImage(Lhero[0].Walking[Lhero[0].IfWalking], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                }
                else
                {
                    g2.DrawImage(Lhero[0].WalkingBack[Lhero[0].IfWalkingBack], Lhero[0].dst, Lhero[0].src, GraphicsUnit.Pixel);
                }
            }
          
            if (showGenie && !genie_dead)
            {
                if (genie_mode == 0)
                {
                    if (Lhero[0].dir == -1)
                    {
                        Rectangle flipDst = new Rectangle(genie_dst.X + genie_dst.Width, genie_dst.Y, -genie_dst.Width, genie_dst.Height);
                        g2.DrawImage(genie_flight[genie_idle_index % genie_flight.Count], flipDst);
                    }
                    else
                    {
                        g2.DrawImage(genie_flight[genie_idle_index % genie_flight.Count], genie_dst);
                    }
                }
                else if (genie_mode == 1)
                {
                    if (Lhero[0].dir == -1)
                    {
                        Rectangle flip_dst = new Rectangle(genie_dst.X + genie_dst.Width, genie_dst.Y, -genie_dst.Width, genie_dst.Height);
                        g2.DrawImage(genie_attack[genie_attack_index], flip_dst);
                    }
                    else
                    {
                        g2.DrawImage(genie_attack[genie_attack_index], genie_dst);
                    }
                }
                else if (genie_mode == 2)
                {
                    if (Lhero[0].dir == -1)
                    {
                        Rectangle flip_dst = new Rectangle(genie_dst.X + genie_dst.Width, genie_dst.Y, -genie_dst.Width, genie_dst.Height);
                        g2.DrawImage(genie_attack[0], flip_dst);
                        int magic_x = genie_dst.X - 140 - magic_xOffset;
                        int magic_y = genie_dst.Y + 12;
                        Rectangle magic_flip = new Rectangle(magic_x + genie_dst.Width, magic_y, -genie_dst.Width, genie_dst.Height);
                        g2.DrawImage(genie_majicAttack[genie_attack_Mi], magic_flip);
                    }
                    else
                    {
                        g2.DrawImage(genie_attack[0], genie_dst);
                        int magic_x = genie_dst.X + 140 + magic_xOffset;
                        int magic_y = genie_dst.Y + 12;
                        g2.DrawImage(genie_majicAttack[genie_attack_Mi], magic_x, magic_y);
                    }
                }
                else if (genie_mode == 3)
                {
                    if (Lhero[0].dir == -1)
                    {
                        Rectangle flipDst = new Rectangle(genie_dst.X + genie_dst.Width, genie_dst.Y, -genie_dst.Width, genie_dst.Height);
                        g2.DrawImage(genie_Death[genie_idle_index % genie_Death.Count], flipDst);
                    }
                    else
                    {
                        g2.DrawImage(genie_Death[genie_idle_index % genie_Death.Count], genie_dst);
                    }
                }
           
            }
        }
        //Handling
        void HandleSwordOne()
        {
            if (FlagSwordOne == 1)
            {
                Lhero[0].IfSwordOne++;

                if (Lhero[0].IfSwordOne >= Lhero[0].SwordOne.Count)
                {
                    FlagSwordOne = 0;
                    Lhero[0].IfSwordOne = 0;
                }
            }
            if (FlagSwordTwo == 1)
            {
                Lhero[0].IfSwordTwo++;

                if (Lhero[0].IfSwordTwo >= Lhero[0].SwordTwo.Count)
                {
                    FlagSwordTwo = 0;
                    Lhero[0].IfSwordTwo = 0;
                }
            }
        }
        void HandleJump()
        {
            if (FlagJump > 0)
            {
                int jumpFrames = Lhero[0].Jump.Count;

                // المرحلة الأولى: التحضير - عرض أول 4 صور فقط، بدون أي تغيير في Y
                if (Lhero[0].IfJump < 4)
                {
                    PrepFrameCounter++;
                    if (PrepFrameCounter >= 1)
                    {
                        Lhero[0].IfJump++;
                        PrepFrameCounter = 0;

                        // عند الانتهاء من التحضير (صورة 4)، نبدأ القفزة فعليًا
                        if (Lhero[0].IfJump == 4)
                        {
                            velocityY = jumpStrength;
                        }
                    }
                }
                else
                {
                    // المرحلة الثانية: البطل بدأ يتحرك (Y يتغير)
                    velocityY += gravity;
                    Lhero[0].dst.Y += (int)velocityY+10;

                    // ➕ أضف الحركة الأفقية أثناء القفز
                    int jumpSpeedX = 20;
                    heroWorldX += jumpSpeedX * jumpDirection;

                    // حدث موضع X للعرض بعد الحركة
                    Lhero[0].dst.X = heroWorldX - cameraX - 600;

                    // تحديث فريمات القفز
                    if (velocityY > 0 && Lhero[0].IfJump < jumpFrames - 1)
                    {
                        Lhero[0].IfJump++;
                    }

                    // التحقق من الهبوط
                    if (Lhero[0].dst.Y >= groundY)
                    {
                        Lhero[0].dst.Y = groundY;
                        FlagJump = 0;
                        velocityY = 0;
                        Lhero[0].IfJump = 0;
                        PrepFrameCounter = 0;
                        jumpCount = 0; // ✅ نرجّع العدّاد
                    }
                  
                }

            }
        }
        void HandleDead()
        {
            if (FlagDead == 1)
            {
                Lhero[0].IfDead++;

                if (Lhero[0].dir == 1)
                {
                    if (Lhero[0].IfDead >= Lhero[0].Dead.Count)
                    {
                        Lhero[0].IfDead = Lhero[0].Dead.Count - 1; // يفضل واقف على آخر فريم موت
                    }
                }
                else
                {
                    if (Lhero[0].IfDead >= Lhero[0].DeadLeft.Count)
                    {
                        Lhero[0].IfDead = Lhero[0].DeadLeft.Count - 1;
                    }
                }
            }
        }

        void HandleTreeEye()//in timer
        {
            if (timerct>20)
            {
               //laser = rnd.Next(0, 10);
                FlagTreeEye=1;
               
            }
            if (timerct >28)
            {
                FlagTreePupil = 1;
              
            }
            if (timerct >29)
            {
                FlagTreePupil = 0;
                FlagTreeEye = 0;
                timerct = 0;
            }
        }
        void HandleFireBall()
        {
            if (FlagFireBall == 1)
            {
                if (Lhero[0].IfFireBall < Lhero[0].FireBall.Count)
                {
                  
                    Lhero[0].IfFireBall++;
                }

                if (Lhero[0].IfFireBall >= Lhero[0].FireBall.Count)
                {
                    Lhero[0].IfFireBall = 0;
                    FlagFireBall = 0;
                    FlagCharge = 1;

                    if (fireballDir == 1)
                    {
                        chargeDst = new Rectangle(
                            Lhero[0].dst.X + Lhero[0].dst.Width - 350,
                            Lhero[0].dst.Y + 200,
                            Lhero[0].dst.Width,
                            Lhero[0].dst.Height);
                    }
                    else if (fireballDir == -1)
                    {
                        chargeDst = new Rectangle(
                            Lhero[0].dst.X - Lhero[0].dst.Width + 350,
                            Lhero[0].dst.Y + 200,
                            Lhero[0].dst.Width,
                            Lhero[0].dst.Height);
                    }
                }
            }

            if (FlagCharge == 1)
            {
                HandleCharge();
            }
        }

        void HandleCharge()
        {
            if (FlagCharge == 1)
            {
                Lhero[0].IfCharge++;

                if (fireballDir == 1)
                    chargeDst.X += 50;
                else if (fireballDir == -1)
                    chargeDst.X -= 50;
            }

            if (Lhero[0].IfCharge >= Lhero[0].Charge.Count)
            {
                Lhero[0].IfCharge = 0;
                FlagCharge = 0;
            }
        }
        void HandleFlame()
        {
            if (FlagFlame == 1)
            {
                Lhero[0].IfFlame++;
            }
            int maxFrames = Lhero[0].dir == 1 ? Lhero[0].Flame.Count : Lhero[0].FlameLeft.Count;
            if (Lhero[0].IfFlame >= maxFrames)
            {
                Lhero[0].IfFlame = 0;
                FlagFlame = 0;
            }

        }

        void HandleHurt()
        {
            if (FlagHurt == 1)
            {
                Lhero[0].IfHurt++;
            }

            if (Lhero[0].dir == 1)
            {
                if (Lhero[0].IfHurt >= Lhero[0].Hurt.Count)
                {
                    Lhero[0].IfHurt = 0;
                    FlagHurt = 0;
                }
            }
            else
            {
                if (Lhero[0].IfHurt >= Lhero[0].HurtLeft.Count)
                {
                    Lhero[0].IfHurt = 0;
                    FlagHurt = 0;
                }
            }

        }
        void HandleStanding()
        {
            if (flagstanding == 1) // البطل واقف
            {
                Lhero[0].IfStanding++;

                if (Lhero[0].dir == 1)
                {
                    if (Lhero[0].IfStanding >= Lhero[0].standing.Count)
                    {
                        Lhero[0].IfStanding = 0;
                    }
                }
                else // dir == -1
                {
                    if (Lhero[0].IfStanding >= Lhero[0].standingBack.Count)
                    {
                        Lhero[0].IfStanding = 0;
                    }
                }
            }

        }
        void handdlejen()
        {
            if (showGenie && !genie_dead)
            {

                genie_offset += genie_direction * 5;
                if (genie_offset > 20 || genie_offset < -20)
                {
                    genie_direction *= -1;
                }
                int genieWidth = Lhero[0].dst.Width / 2;
                int genieHeight = Lhero[0].dst.Height / 2;
                genie_dst = new Rectangle(Lhero[0].dst.X - 180 + (Lhero[0].dst.Width - genieWidth) / 2, Lhero[0].dst.Y - genieHeight + 300 + genie_offset, genieWidth, genieHeight);
                // flight
                if (genie_mode == 0)
                {
                    genie_idle_index++;
                    genie_idle_index %= genie_flight.Count;

                    genie_timerct++;
                    if (genie_timerct >= genieFlight_duration)
                    {
                        genie_mode = 1;
                        genie_timerct = 0;
                    }
                }

                // attack
                else if (genie_mode == 1)
                {
                    genie_attack_index++;
                    if (genie_attack_index >= genie_attack.Count)
                    {
                        genie_attack_index = 0;
                        genie_mode = 2;
                    }
                }
                // magic attack
                else if (genie_mode == 2)
                {
                    genie_attack_Mi++;
                    magic_xOffset += magic_xSpeed;

                    if (genie_attack_Mi >= genie_majicAttack.Count)
                    {
                        genie_attack_Mi = 0;
                        magic_xOffset = 0;
                        genieAtt_repeatCounter++;

                        if (genieAtt_repeatCounter >= 3)
                        {
                            genie_mode = 3;
                        }
                        else
                        {
                            genie_mode = 1;
                            genie_timerct = 0;
                        }
                    }
                }
                // death
                else if (genie_mode == 3)
                {
                    geniedeath_delayCt++;
                    if (geniedeath_delayCt >= geniedeath_delay)
                    {
                        geniedeath_delayCt = 0;
                        genie_idle_index++;
                        if (genie_idle_index >= genie_Death.Count)
                        {
                            genie_dead = true;
                            showGenie = false;
                        }
                    }
                }

            }
        }
        
        //////////////////////////
       




        private void Timer_Tick(object sender, EventArgs e)
        {
            timerct++;

            HandleSwordOne();
         
            HandleJump();

            HandleStanding();


            HandleDead();

            HandleHurt();

            HandleFireBall();

            HandleFlame();

            handdlejen();

            HandleTreeEye();
            UpdateFireBalls();

            DrawDubbleBuffer();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            OFF = new Bitmap(this.Width, this.Height);
            Createhero();
            CreateBackGrounds();
            CreateEyesOfTree();
            createGenie();
        }
        void UpdateFireBalls()
        {
            for (int i = Lfireballs.Count - 1; i >= 0; i--)
            {
                FireBall fb = Lfireballs[i];

                if (fb.stage == 0)
                {
                    fb.frameindex++;
                    if (fb.frameindex >= Lhero[0].FireBall.Count)
                    {
                        fb.stage = 1;
                        fb.frameindex = 0;
                    }
                }
                else if (fb.stage == 1)
                {
                    fb.chargedst.X += fb.direction * 50;
                    fb.frameindex++;

                    if (fb.frameindex >= Lhero[0].Charge.Count)
                    {
                        Lfireballs.RemoveAt(i);
                    }
                }
            }

            if (FlagCasting == 1)
            {
                CastingCounter++;
                if (CastingCounter >= MaxCastingFrames)
                {
                    FlagCasting = 0;
                }
            }
        }


        private void Form1_Load1(object sender, EventArgs e)
        {
           
        }
        void DrawDubbleBuffer()
        {
            Graphics g1 = this.CreateGraphics();
            Graphics g2 = Graphics.FromImage(OFF);
            DrawScene(g2);
            g1.DrawImage(OFF, 0, 0);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubbleBuffer();
        }
        void Scrolling(Graphics g2)
        {

            int screenW = this.Width;
            int screenH = this.Height;

            for (int i = 0; i < LBackGrounds.Count; i++)
            {
                AdvImgBackGround bg = LBackGrounds[i];
                int worldX = i * screenW;
                int worldRight = worldX + screenW;
                int cameraRight = cameraX + screenW;

                // تحقق يدوي من تقاطع الخلفية مع الكاميرا
                if (worldRight > cameraX && worldX < cameraRight)
                {
                    int intersectX;
                    if (worldX >= cameraX)
                        intersectX = worldX;
                    else
                        intersectX = cameraX;

                    int intersectRight;
                    if (worldRight <= cameraRight)
                        intersectRight = worldRight;
                    else
                        intersectRight = cameraRight;

                    int intersectW = intersectRight - intersectX;

                    int srcX = intersectX - worldX;
                    int dstX = intersectX - cameraX;

                    Rectangle srcRect = new Rectangle(srcX, 0, intersectW, screenH);
                    Rectangle dstRect = new Rectangle(dstX, 0, intersectW, screenH);

                    g2.DrawImage(bg.img, dstRect, srcRect, GraphicsUnit.Pixel);
                }
            }
        }
    }
}
