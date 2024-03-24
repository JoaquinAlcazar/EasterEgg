using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;

namespace EasterEgg
{
    public class Character
    {
        public string? Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Spd { get; set; }

        public Character(string? name, int health, int atk, int def, int spd)
        {
            Name = name;
            Level = 1;
            Health = health;
            Atk = atk;
            Def = def;
            Spd = spd;
        }
        public static Character CreateCharacter()
        {
            Console.WriteLine("Enter your character's name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your character's health:");
            int health = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your character's attack:");
            int atk = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your character's defense:");
            int def = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your character's speed:");
            int spd = Convert.ToInt32(Console.ReadLine());

            Character player = new Character(name, health, atk, def, spd);
            player.SaveCharacterInXML();
            Console.WriteLine($"Character {player.Name} created!");
            return player;
        }
        public override string ToString()
        {
            return $"Name: {Name}\n" +
                $"Level: {Level}\n" +
                $"Health: {Health}\n" +
                $"Attack: {Atk}\n" +
                $"Defense: {Def}\n" +
                $"Speed: {Spd}";
        }
        public void SaveCharacterInXML()
        {
            try
            {
                string path = "Characters.xml";

                if (File.Exists(path))
                {
                    XDocument doc = XDocument.Load(path);
                    if (doc.Element("characters") == null)
                    {
                        doc.Add(new XElement("characters"));
                    }
                    XElement character = new XElement("Character",
                    new XElement("Name", Name),
                    new XElement("Level", Level),
                    new XElement("Health", Health),
                    new XElement("Atk", Atk),
                    new XElement("Def", Def),
                    new XElement("Spd", Spd));
                    doc.Element("characters")?.Add(character);
                    doc.Save(path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public void Battle(Character enemy)
        {
            int playerDamage = Atk - enemy.Def;
            int enemyDamage = enemy.Atk - Def;
            while (Health > 0 && enemy.Health > 0)
            {
                enemy.Health -= playerDamage;
                Health -= enemyDamage;
            }
            if (enemy.Health <= 0)
            {
                Console.WriteLine($"{enemy.Name} defeated! {Name} won!");
            }
            else
            {
                Console.WriteLine($"{Name} defeated! {enemy.Name} won!");
            }
        }

        public void Update(string name, string atribute, string value)
        {
            Console.WriteLine(atribute + " updated!");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Characters.xml");
            XmlNodeList nodes = xmlDoc.SelectNodes("//Character");
            foreach (XmlNode node in nodes)
            {
                XmlNode nameNode = node.SelectSingleNode("Name");
                if (nameNode != null && nameNode.InnerText == name)
                {
                    Console.WriteLine(nameNode.InnerText);
                    XmlNode updateNode = node.SelectSingleNode(atribute);
                    updateNode.InnerText = value;
                }

            }
            xmlDoc.Save("Characters.xml");

        }

        public static void Battle(string char1, string char2)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Characters.xml");
            XmlNodeList nodes = xmlDoc.SelectNodes("//Character");
            int atk1 = 0, atk2 = 0, def1 = 0, def2 = 0, hp1 = 0, hp2 = 0;

            foreach (XmlNode node in nodes)
            {
                XmlNode nameNode = node.SelectSingleNode("Name");
                if (nameNode != null && nameNode.InnerText == char1)
                {
                    XmlNode atkNode = node.SelectSingleNode("Atk");
                    atk1 = Convert.ToInt32(atkNode.InnerText);
                    XmlNode defNode = node.SelectSingleNode("Def");
                    def1 = Convert.ToInt32(defNode.InnerText);
                    XmlNode hpNode = node.SelectSingleNode("Health");
                    hp1 = Convert.ToInt32(hpNode.InnerText);

                }
                else if (nameNode != null && nameNode.InnerText == char2)
                {
                    XmlNode atk2Node = node.SelectSingleNode("Atk");
                    atk2 = Convert.ToInt32(atk2Node.InnerText);
                    XmlNode def2Node = node.SelectSingleNode("Def");
                    def2 = Convert.ToInt32(def2Node.InnerText);
                    XmlNode hp2Node = node.SelectSingleNode("Health");
                    hp2 = Convert.ToInt32(hp2Node.InnerText);
                }


            }
            while (hp1 > 0 && hp2 > 0)
            {
                hp1 -= atk2 - def1;
                hp2 -= atk1 - def2;
            }
            if (hp1 <= 0 && hp2 <= 0)
            {
                Console.WriteLine($"Han empatat!");
            }
            else if (hp1 <= 0)
            {
                Console.WriteLine($"{char2} won the battle!");
            }
            else if (hp2 <= 0)
            {
                Console.WriteLine($"{char1} won the battle!");
            }
        }
    }
}

