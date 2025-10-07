namespace lab4_advancedC_
{

    //match logic :
    /*
    1- match contains players , refree , ball
    2- refree raises start ,all players move   
    players can kick the ball , once the palyer injured the refree raises and other player 
    of the alternatives enters
    3- if the ball goes out the field (70 * 70 ) m^2, refree  raises Ball_out_of_field
       , if the ball position between  net (20 , 20) , refree raises Goalllll 
       , if goals > 3 refree raises end match 
    
     */


    //                           ___________________________(70,70)
    //                          |                           |
    //               (0,40)     |_____                      |
    //                          |     |                     |
    //              (0,20)      |_____|(20,20)              |
    //                          |                           |
    //        (x,y)=(0,0)       |___________________________|(70,0)


    class Ball
    {
        int x;
        int y;
        static int goals = 0;

        public int X
        {
            get
            { return x; }
            set
            { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public Player p { get; set; }

        public Ball(int x, int y, Player p)
        {
            this.x = x; this.y = y;
            if (p != null)
            {
                this.p = p;
                this.p.ball_kick += kick_ball;
                this.p.ball_position += check_position;
            }
        }

        public void kick_ball(int dx, int dy)
        {
            x += dx;
            y += dy;
            Console.WriteLine("Ball kicked...");

        }

        public void check_position(object sender, EventArgs e)
        {

            //Ball b = (Ball)sender;

            if (x > 70 || x < 0 || y < 0 || y > 70)
            {
                Console.WriteLine($"refree {p.Rf._name} says ball out of bound ");

                reset_ball();
            }
            else if (x > 0 && x < 20 && y > 20 && y < 40)
            {
                goals++;
                Console.WriteLine($"refree {p.Rf._name} says Goalllll  ");
                if (goals > 3)
                {
                    Console.WriteLine($"refree {p.Rf._name} Ended the match  ");
                }
            }
            Console.WriteLine("goal");

        }
        public void reset_ball()
        {
            x = 35;
            y = 35;
        }


    }
    class Refree
    {
        string name;
        public event Action<int, int> start_match; // move all players by dx,dy : need Action not EventHandler
        public Refree(string name)
        {
            this.name = name;

        }
        public string _name
        {
            set
            {
                name = value;
            }
            get
            {
                return name;
            }
        }
        public void starting(int dx = 5, int dy = 3)
        {
            start_match?.Invoke(dx, dy);

        }

        
        public void change_player(object sender, EventArgs e)
        {
            Player injuredPlayer = sender as Player;
            
            Console.WriteLine($"refree {name} says : player {injuredPlayer.name} is injured and needs alternative");

            Player alternative = new Player("Ali", -1, -1, false, injuredPlayer.Rf, injuredPlayer.Bl);

            injuredPlayer.change_player(alternative);
            Console.WriteLine($"{injuredPlayer.name} is replaced by {alternative.name} ");


        }



    }

    class Player
    {

        int x;
        int y;
        bool isInjured;
        Ball ball;
        Refree refree;
        public event Action<int, int> ball_kick;
        public event EventHandler player_injured;
        public event EventHandler ball_position;
        public string name { get; set; }

        public Player(string name, int x, int y, bool isInjured, Refree r, Ball b) // initail position for players 
        {
            this.name = name; this.x = x; this.y = y;
            this.isInjured = isInjured;
            ball = b;
            refree = r;
            ball_kick += b.kick_ball;
            ball_position += b.check_position;
            player_injured += r.change_player; //it’s not a must to have the publisher (event)
                                               //and subscriber (handler) in different classes.
                                               //You can absolutely have them in the same class,
                                               //or even have multiple subscribers in the same or
                                               //different classes.
            r.start_match += move_player;


        }
        public void move_player(int dx = 0, int dy = 0)
        {

            x += dx;
            y += dy;
            Console.WriteLine($" player : {name} moved  , now at x ={x}, y={y} ... ");

            //if the ball dim = player dim == > kick the ball, check its position, move the player
            if (x == ball.X && y == ball.Y)
            {
                ball_kick?.Invoke(5, 15);
                ball_position?.Invoke(ball,null); //null or EventArgs.Empty
                //move_player(7, -5); may cause recursive call
                x += 7;
                y -= 5;
            }

            //check if a player x > 70  , y>70
            if (x > 70) x -= 30;
            if (y > 70) y -= 30;
        }

        public void change_player(Player p)
        {
            this.x = p.x;
            this.y = p.y;
            this.isInjured = p.isInjured;
        }
        public bool IsInjured
        {
            get
            {
                return isInjured;
            }
            set
            {
                isInjured = value;
                if (isInjured == true) // referee should raise player {name} need alternative
                {
                    player_injured?.Invoke(this, null);
                }
            }
        }

        public Refree Rf
        {
            get
            {
                return refree;
            }
            set
            {
                refree = value;
            }
        }
        public Ball Bl
        {
            get
            {
                return ball;
            }
            set
            {
                ball = value;
            }
        }


    }
    internal class Program
    {

        static void Main(string[] args)
        {
            //create refree 
            Refree r1 = new Refree("Farok");

            //initialize the ball 
            Ball ball = new Ball(5, 10, null); //to avoid circular 

            //create players in match 
            Player p1 = new Player("Aboutrika", 50, 30, false, r1, ball);
            Player p2 = new Player("Moteab", 40, 60, false, r1, ball);
            Player p3 = new Player("Hassan", 60, 10, false, r1, ball);

            ball.p = p1; //to avoid circular 

            r1.starting(); //refree starts -> so all players moved 
            Console.WriteLine("--------------------------");


            //force player to kick_ball + check_position
            ball.X = 55;
            ball.Y = 33;
            p1.move_player(0, 0);
            Console.WriteLine("--------------------------");

            //kick + out of field 
            ball.X = 80; ball.Y = 80;
            p2.move_player(35, 17);

            Console.WriteLine("--------------------------");

            //kick +  goal 
            ball.X = 57; ball.Y = 45;
            p2.move_player(0, 0);
            Console.WriteLine("--------------------------");

            //check alternatives 
            p2.IsInjured = true;

            Console.WriteLine("--------------------------");

            Console.ReadKey();





        }
    }
}
