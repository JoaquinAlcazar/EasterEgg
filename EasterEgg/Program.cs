using System;
using System.Collections.Generic;
using System.Xml;

namespace EasterEgg
{
    public class Program
    {
        public static void Main()
        {

            const string MsgWelcome = "Hello hero! Do you want to create a character? \n1.YES\n2.No";
            const string again = "Do you want to create another character or to update a character?\n1.CREATE\n2.GO BATTLE\n3.UPDATE";
            const string byebye = "Thanks for using. Here are your characters";
            const string updName = "Select a character to update. (Case sensitive)";
            const string attUpdateMenu = "Select an attribute to update:\n" +
                "1.Name\n" +
                "2.Level\n" +
                "3.Health\n" +
                "4.Attack\n" +
                "5.Defense\n" +
                "6.Speed\n";
            const string newValue = "Select new value";
            const string charNotFound = "Error 404 - Character not found";
            const string selectChar1 = "Select character 1";
            const string selectChar2 = "Select character 2";

            XmlWriter doc = XmlWriter.Create("Characters.xml");
            doc.WriteStartDocument();
            doc.WriteStartElement("characters");
            doc.WriteString("");
            doc.WriteEndElement();
            doc.Close();
            int selection = 1;
            int updateSelection = 0;
            string updValue = "";


            List<Character> list = new List<Character>();
            bool characterExists = false;
            string characterToUpd = "";

            string char1 = "";
            string char2 = "";

            Console.WriteLine(MsgWelcome);
            selection = Convert.ToInt32(Console.ReadLine());
            while (selection == 1 || selection == 3) {
                switch (selection)
                {
                    case 1:
                        list.Add(Character.CreateCharacter());
                        Console.WriteLine(again);
                        selection = Convert.ToInt32(Console.ReadLine());
                        break;
                    case 3:
                        Console.WriteLine(updName);
                        characterToUpd = Console.ReadLine();
                        foreach (Character personaje in list)
                        {
                            if (personaje.Name == characterToUpd)
                            {
                                characterExists = true;
                                Console.WriteLine(attUpdateMenu);
                                while (updateSelection < 1 || updateSelection > 6)
                                {
                                    updateSelection = Convert.ToInt32(Console.ReadLine());
                                    switch (updateSelection)
                                    {
                                        case 1:
                                            Console.WriteLine(newValue);
                                            updValue = Console.ReadLine();
                                            personaje.Update(characterToUpd, "Name", updValue);
                                            break;
                                        case 2:
                                            Console.WriteLine(newValue);
                                            updValue = Console.ReadLine();
                                            personaje.Update(characterToUpd, "Level", updValue);
                                            break;
                                        case 3:
                                            Console.WriteLine(newValue);
                                            updValue = Console.ReadLine();
                                            personaje.Update(characterToUpd, "Health", updValue);
                                            break;
                                        case 4:
                                            Console.WriteLine(newValue);
                                            updValue = Console.ReadLine();
                                            personaje.Update(characterToUpd, "Attack", updValue);
                                            break;
                                        case 5:
                                            Console.WriteLine(newValue);
                                            updValue = Console.ReadLine();
                                            personaje.Update(characterToUpd, "Defense", updValue);
                                            break;
                                        case 6:
                                            Console.WriteLine(newValue);
                                            updValue = Console.ReadLine();
                                            personaje.Update(characterToUpd, "Speed", updValue);
                                            break;
                                        default: Console.WriteLine("Invalid option, select a valid one"); break;
                                    }
                                }

                            }
                        }
                        if (!characterExists) Console.WriteLine(charNotFound);
                        characterExists = false;
                        Console.WriteLine(again);
                        selection = Convert.ToInt32(Console.ReadLine());
                        break;
                }
                
            }
            if (selection < 1 || selection > 3) Console.WriteLine("Invalid option, reset the program.");
            if (list.Count < 2) Console.WriteLine("Not enough characters to battle");
            else
            {
                Console.WriteLine(selectChar1);
                char1 = Console.ReadLine();
                Console.WriteLine(selectChar2);
                char2 = Console.ReadLine();

                foreach(Character personaje in list)
                {
                    if(personaje.Name == char1)
                    {
                        foreach (Character perso in list)
                        {
                            if (perso.Name == char2)
                            {
                                personaje.Battle(perso);
                            }
                        }
                    }
                }
            }
            if (selection == 2) Console.WriteLine(byebye);

            XmlReader reader = XmlReader.Create("Characters.xml");
            try
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Name")
                    {
                        reader.Read();
                        Console.WriteLine($"Name: {reader.Value}");
                    }
                    else if (reader.NodeType == XmlNodeType.Element && reader.Name == "Level")
                    {
                        reader.Read();
                        Console.WriteLine($"Level: {reader.Value}");
                    }
                    else if (reader.NodeType == XmlNodeType.Element && reader.Name == "Health")
                    {
                        reader.Read();
                        Console.WriteLine($"Health: {reader.Value}");
                    }
                    else if (reader.NodeType == XmlNodeType.Element && reader.Name == "Atk")
                    {
                        reader.Read();
                        Console.WriteLine($"Attack: {reader.Value}");
                    }
                    else if (reader.NodeType == XmlNodeType.Element && reader.Name == "Def")
                    {
                        reader.Read();
                        Console.WriteLine($"Defense: {reader.Value}");
                    }
                    else if (reader.NodeType == XmlNodeType.Element && reader.Name == "Spd")
                    {
                        reader.Read();
                        Console.WriteLine($"Speed: {reader.Value}");
                        Console.WriteLine();
                    }
                }

            }
            finally
            {
                reader.Close();
            }
        }
    }
}
