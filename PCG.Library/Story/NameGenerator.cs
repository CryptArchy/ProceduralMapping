﻿using PCG.Library.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Story
{
    public class NameGenerator
    {
        private MarkovChain<char> Chain;

        public NameGenerator()
        {
            Chain = new MarkovChain<char>(2);

            foreach (var name in MaleUsNames)
                Chain.Add(name);

            foreach (var name in FemaleUsNames)
                Chain.Add(name);
        }

        public string MakeName(int length)
        {
            return new string(Chain.Chain().Take(length).ToArray());
        }

        private List<Phoneme> AllPhonemes()
        {
            var result = new List<Phoneme>()
            {
                new Phoneme("a", "ah", "a"), //open front unrounded vowel
                new Phoneme("b", "b", "b"), //voiced bilabial plosive
            };
            return result;
        }

        private class Phoneme
        {
            public string XSampa { get; set; }
            public string IPA { get; set; }
            public string Pronounciation { get; set; }
            public List<string> Spellings { get; set; }

            public Phoneme(string xsampa, string pronounciation, params string[] spellings)
            {
                XSampa = xsampa;
                Pronounciation = pronounciation;
                Spellings = new List<string>(spellings);
            }
        }

        #region Constants
        private List<String> MaleUsNames = new List<string>()
        {
            "Jacob", "Mason", "William", "Jayden", "Noah", "Michael", "Ethan", "Alexander", "Aiden", "Daniel", 
            "Anthony", "Matthew", "Elijah", "Joshua", "Liam", "Andrew", "James", "David", "Benjamin", "Logan", 
            "Christopher", "Joseph", "Jackson", "Gabriel", "Ryan", "Samuel", "John", "Nathan", "Lucas", "Christian", 
            "Jonathan", "Caleb", "Dylan", "Landon", "Isaac", "Gavin", "Brayden", "Tyler", "Luke", "Evan", 
            "Carter", "Nicholas", "Isaiah", "Owen", "Jack", "Jordan", "Brandon", "Wyatt", "Julian", "Aaron", 
            "Jeremiah", "Angel", "Cameron", "Connor", "Hunter", "Adrian", "Henry", "Eli", "Justin", "Austin", 
            "Robert", "Charles", "Thomas", "Zachary", "Jose", "Levi", "Kevin", "Sebastian", "Chase", "Ayden", 
            "Jason", "Ian", "Blake", "Colton", "Bentley", "Dominic", "Xavier", "Oliver", "Parker", "Josiah", 
            "Adam", "Cooper", "Brody", "Nathaniel", "Carson", "Jaxon", "Tristan", "Luis", "Juan", "Hayden", 
            "Carlos", "Jesus", "Nolan", "Cole", "Alex", "Max", "Grayson", "Bryson", "Diego", "Jaden", 
            "Vincent", "Easton", "Eric", "Micah", "Kayden", "Jace", "Aidan", "Ryder", "Ashton", "Bryan", 
            "Riley", "Hudson", "Asher", "Bryce", "Miles", "Kaleb", "Giovanni", "Antonio", "Kaden", "Colin", 
            "Kyle", "Brian", "Timothy", "Steven", "Sean", "Miguel", "Richard", "Ivan", "Jake", "Alejandro", 
            "Santiago", "Axel", "Joel", "Maxwell", "Brady", "Caden", "Preston", "Damian", "Elias", "Jaxson", 
            "Jesse", "Victor", "Patrick", "Jonah", "Marcus", "Rylan", "Emmanuel", "Edward", "Leonardo", "Cayden", 
            "Grant", "Jeremy", "Braxton", "Gage", "Jude", "Wesley", "Devin", "Roman", "Mark", "Camden", 
            "Kaiden", "Oscar", "Alan", "Malachi", "George", "Peyton", "Leo", "Nicolas", "Maddox", "Kenneth", 
            "Mateo", "Sawyer", "Collin", "Conner", "Cody", "Andres", "Declan", "Lincoln", "Bradley", "Trevor", 
            "Derek", "Tanner", "Silas", "Eduardo", "Seth", "Jaiden", "Paul", "Jorge", "Cristian", "Garrett", 
            "Travis", "Abraham", "Omar", "Javier", "Ezekiel", "Tucker", "Harrison", "Peter", "Damien", "Greyson", 
            "Avery", "Kai", "Weston", "Ezra", "Xander", "Jaylen", "Corbin", "Fernando", "Calvin", "Jameson", 
            "Francisco", "Maximus", "Josue", "Ricardo", "Shane", "Trenton", "Cesar", "Chance", "Drake", "Zane", 
            "Israel", "Emmett", "Jayce", "Mario", "Landen", "Kingston", "Spencer", "Griffin", "Stephen", "Manuel", 
            "Theodore", "Erick", "Braylon", "Raymond", "Edwin", "Charlie", "Abel", "Myles", "Bennett", "Johnathan", 
            "Andre", "Alexis", "Edgar", "Troy", "Zion", "Jeffrey", "Hector", "Shawn", "Lukas", "Amir", 
            "Tyson", "Keegan", "Kyler", "Donovan", "Graham", "Simon", "Everett", "Clayton", "Braden", "Luca", 
            "Emanuel", "Martin", "Brendan", "Cash", "Zander", "Jared", "Ryker", "Dante", "Dominick", "Lane", 
            "Kameron", "Elliot", "Paxton", "Rafael", "Andy", "Dalton", "Erik", "Sergio", "Gregory", "Marco", 
            "Emiliano", "Jasper", "Johnny", "Dean", "Drew", "Caiden", "Skyler", "Judah", "Maximiliano", "Aden", 
            "Fabian", "Zayden", "Brennan", "Anderson", "Roberto", "Reid", "Quinn", "Angelo", "Holden", "Cruz", 
            "Derrick", "Grady", "Emilio", "Finn", "Elliott", "Pedro", "Amari", "Frank", "Rowan", "Lorenzo", 
            "Felix", "Corey", "Dakota", "Colby", "Braylen", "Dawson", "Brycen", "Allen", "Jax", "Brantley", 
            "Ty", "Malik", "Ruben", "Trey", "Brock", "Colt", "Dallas", "Joaquin", "Leland", "Beckett", 
            "Jett", "Louis", "Gunner", "Adan", "Jakob", "Cohen", "Taylor", "Arthur", "Marcos", "Marshall", 
            "Ronald", "Julius", "Armando", "Kellen", "Dillon", "Brooks", "Cade", "Danny", "Nehemiah", "Beau", 
            "Jayson", "Devon", "Tristen", "Enrique", "Randy", "Gerardo", "Pablo", "Desmond", "Raul", "Romeo", 
            "Milo", "Julio", "Kellan", "Karson", "Titus", "Keaton", "Keith", "Reed", "Ali", "Braydon", 
            "Dustin", "Scott", "Trent", "Waylon", "Walter", "Donald", "Ismael", "Phillip", "Iker", "Esteban", 
            "Jaime", "Landyn", "Darius", "Dexter", "Matteo", "Colten", "Emerson", "Phoenix", "King", "Izaiah", 
            "Karter", "Albert", "Jerry", "Tate", "Larry", "Saul", "Payton", "August", "Jalen", "Enzo", 
            "Jay", "Rocco", "Kolton", "Russell", "Leon", "Philip", "Gael", "Quentin", "Tony", "Mathew", 
            "Kade", "Gideon", "Dennis", "Damon", "Darren", "Kason", "Walker", "Jimmy", "Alberto", "Mitchell", 
            "Alec", "Rodrigo", "Casey", "River", "Maverick", "Amare", "Brayan", "Mohamed", "Issac", "Yahir", 
            "Arturo", "Moises", "Maximilian", "Knox", "Barrett", "Davis", "Gustavo", "Curtis", "Hugo", "Reece", 
            "Chandler", "Mauricio", "Jamari", "Abram", "Uriel", "Bryant", "Archer", "Kamden", "Solomon", "Porter", 
            "Zackary", "Adriel", "Ryland", "Lawrence", "Noel", "Alijah", "Ricky", "Ronan", "Leonel", "Maurice", 
            "Chris", "Atticus", "Brenden", "Ibrahim", "Zachariah", "Khalil", "Lance", "Marvin", "Dane", "Bruce", 
            "Cullen", "Orion", "Nikolas", "Pierce", "Kieran", "Braeden", "Kobe", "Finnegan", "Remington", "Muhammad", 
            "Prince", "Orlando", "Alfredo", "Mekhi", "Sam", "Rhys", "Jacoby", "Eddie", "Zaiden", "Ernesto", 
            "Joe", "Kristopher", "Jonas", "Gary", "Jamison", "Nico", "Johan", "Giovani", "Malcolm", "Armani", 
            "Warren", "Gunnar", "Ramon", "Franklin", "Kane", "Byron", "Cason", "Brett", "Ari", "Deandre", 
            "Finley", "Justice", "Douglas", "Cyrus", "Gianni", "Talon", "Camron", "Cannon", "Nash", "Dorian", 
            "Kendrick", "Moses", "Arjun", "Sullivan", "Kasen", "Dominik", "Ahmed", "Korbin", "Roger", "Royce", 
            "Quinton", "Salvador", "Isaias", "Skylar", "Raiden", "Terry", "Brodie", "Tobias", "Morgan", "Frederick", 
            "Madden", "Conor", "Reese", "Braiden", "Kelvin", "Julien", "Kristian", "Rodney", "Wade", "Davion", 
            "Nickolas", "Xzavier", "Alvin", "Asa", "Alonzo", "Ezequiel", "Boston", "Nasir", "Nelson", "Jase", 
            "London", "Mohammed", "Rhett", "Jermaine", "Roy", "Matias", "Ace", "Chad", "Moshe", "Aarav", 
            "Keagan", "Aldo", "Blaine", "Marc", "Rohan", "Bently", "Trace", "Kamari", "Layne", "Carmelo", 
            "Demetrius", "Lawson", "Nathanael", "Uriah", "Terrance", "Ahmad", "Jamarion", "Shaun", "Kale", "Noe", 
            "Carl", "Jaydon", "Callen", "Micheal", "Jaxen", "Lucian", "Jaxton", "Rory", "Quincy", "Guillermo", 
            "Javon", "Kian", "Wilson", "Jeffery", "Joey", "Kendall", "Harper", "Jensen", "Mohammad", "Dayton", 
            "Billy", "Jonathon", "Jadiel", "Willie", "Jadon", "Clark", "Rex", "Francis", "Kash", "Malakai", 
            "Terrell", "Melvin", "Cristopher", "Layton", "Ariel", "Sylas", "Gerald", "Kody", "Messiah", "Semaj", 
            "Triston", "Bentlee", "Lewis", "Marlon", "Tomas", "Aidyn", "Tommy", "Alessandro", "Isiah", "Jagger", 
            "Nikolai", "Omari", "Sincere", "Cory", "Rene", "Terrence", "Harley", "Kylan", "Luciano", "Aron", 
            "Felipe", "Reginald", "Tristian", "Urijah", "Beckham", "Jordyn", "Kayson", "Neil", "Osvaldo", "Aydin", 
            "Ulises", "Deacon", "Giovanny", "Case", "Daxton", "Will", "Lee", "Makai", "Raphael", "Tripp", 
            "Kole", "Channing", "Santino", "Stanley", "Allan", "Alonso", "Jamal", "Jorden", "Davin", "Soren", 
            "Aryan", "Aydan", "Camren", "Jasiah", "Ray", "Ben", "Jon", "Bobby", "Darrell", "Markus", 
            "Branden", "Hank", "Mathias", "Adonis", "Darian", "Jessie", "Marquis", "Vicente", "Zayne", "Kenny", 
            "Raylan", "Jefferson", "Steve", "Wayne", "Leonard", "Kolby", "Ayaan", "Emery", "Harry", "Rashad", 
            "Adrien", "Dax", "Dwayne", "Samir", "Zechariah", "Yusuf", "Ronnie", "Tristin", "Benson", "Memphis", 
            "Lamar", "Maxim", "Bowen", "Ellis", "Javion", "Tatum", "Clay", "Alexzander", "Draven", "Odin", 
            "Branson", "Elisha", "Rudy", "Zain", "Rayan", "Sterling", "Brennen", "Jairo", "Brendon", "Kareem", 
            "Rylee", "Winston", "Jerome", "Kyson", "Lennon", "Luka", "Crosby", "Deshawn", "Roland", "Zavier", 
            "Cedric", "Vance", "Niko", "Gauge", "Kaeden", "Killian", "Vincenzo", "Teagan", "Trevon", "Kymani", 
            "Valentino", "Abdullah", "Bo", "Darwin", "Hamza", "Kolten", "Edison", "Jovani", "Augustus", "Gavyn", 
            "Toby", "Davian", "Rogelio", "Matthias", "Brent", "Hayes", "Brogan", "Jamir", "Damion", "Emmitt", 
            "Landry", "Chaim", "Jaylin", "Yosef", "Kamron", "Lionel", "Van", "Bronson", "Casen", "Junior", 
            "Misael", "Yandel", "Alfonso", "Giancarlo", "Rolando", "Abdiel", "Aaden", "Deangelo", "Duncan", "Ishaan", 
            "Jamie", "Maximo", "Cael", "Conrad", "Ronin", "Xavi", "Dominique", "Ean", "Tyrone", "Chace", 
            "Craig", "Mayson", "Quintin", "Derick", "Bradyn", "Izayah", "Zachery", "Westin", "Alvaro", "Johnathon", 
            "Ramiro", "Konner", "Lennox", "Marcelo", "Blaze", "Eugene", "Keenan", "Bruno", "Deegan", "Rayden", 
            "Cale", "Camryn", "Eden", "Jamar", "Leandro", "Sage", "Marcel", "Jovanni", "Rodolfo", "Seamus", 
            "Cain", "Damarion", "Harold", "Jaeden", "Konnor", "Jair", "Callum", "Rowen", "Rylen", "Arnav", 
            "Ernest", "Gilberto", "Irvin", "Fisher", "Randall", "Heath", "Justus", "Lyric", "Masen", "Amos", 
            "Frankie", "Harvey", "Kamryn", "Alden", "Hassan", "Salvatore", "Theo", "Darien", "Gilbert", "Krish", 
            "Mike", "Todd", "Jaidyn", "Isai", "Samson", "Cassius", "Hezekiah", "Makhi", "Antoine", "Darnell", 
            "Remy", "Stefan", "Camdyn", "Kyron", "Callan", "Dario", "Jedidiah", "Leonidas", "Deven", "Fletcher", 
            "Sonny", "Reagan", "Yadiel", "Jerimiah", "Efrain", "Sidney", "Santos", "Aditya", "Brenton", "Brysen", 
            "Nixon", "Tyrell", "Vaughn", "Elvis", "Freddy", "Demarcus", "Gaige", "Jaylon", "Gibson", "Thaddeus", 
            "Zaire", "Coleman", "Roderick", "Jabari", "Zackery", "Agustin", "Alfred", "Arlo", "Braylin", "Leighton", 
            "Turner", "Arian", "Clinton", "Legend", "Miller", "Quinten", "Mustafa", "Jakobe", "Lathan", "Otto", 
            "Blaise", "Vihaan", "Enoch", "Ross", "Brice", "Houston", "Rey", "Benton", "Bodhi", "Graysen", 
            "Johann", "Reuben", "Crew", "Darryl", "Donte", "Flynn", "Jaycob", "Jean", "Maxton", "Anders", 
            "Hugh", "Ignacio", "Ralph", "Trystan", "Devan", "Franco", "Mariano", "Tyree", "Bridger", "Howard", 
            "Jaydan", "Brecken", "Joziah", "Valentin", "Broderick", "Maxx", "Elian", "Eliseo", "Haiden", "Tyrese", 
            "Zeke", "Keon", "Maksim", "Coen", "Cristiano", "Hendrix", "Damari", "Princeton", "Davon", "Deon", 
            "Kael", "Dimitri", "Jaron", "Jaydin", "Kyan", "Corban", "Kingsley", "Major", "Pierre", "Yehuda", 
            "Cayson", "Dangelo", "Jeramiah", "Kamren", "Kohen", "Camilo", "Cortez", "Keyon", "Malaki", "Ethen", 
        };

        private List<string> FemaleUsNames = new List<string>()
        {
            "Sophia", "Isabella", "Emma", "Olivia", "Ava", "Emily", "Abigail", "Madison", "Mia", "Chloe", 
            "Elizabeth", "Ella", "Addison", "Natalie", "Lily", "Grace", "Samantha", "Avery", "Sofia", "Aubrey", 
            "Brooklyn", "Lillian", "Victoria", "Evelyn", "Hannah", "Alexis", "Charlotte", "Zoey", "Leah", "Amelia", 
            "Zoe", "Hailey", "Layla", "Gabriella", "Nevaeh", "Kaylee", "Alyssa", "Anna", "Sarah", "Allison", 
            "Savannah", "Ashley", "Audrey", "Taylor", "Brianna", "Aaliyah", "Riley", "Camila", "Khloe", "Claire", 
            "Sophie", "Arianna", "Peyton", "Harper", "Alexa", "Makayla", "Julia", "Kylie", "Kayla", "Bella", 
            "Katherine", "Lauren", "Gianna", "Maya", "Sydney", "Serenity", "Kimberly", "Mackenzie", "Autumn", "Jocelyn", 
            "Faith", "Lucy", "Stella", "Jasmine", "Morgan", "Alexandra", "Trinity", "Molly", "Madelyn", "Scarlett", 
            "Andrea", "Genesis", "Eva", "Ariana", "Madeline", "Brooke", "Caroline", "Bailey", "Melanie", "Kennedy", 
            "Destiny", "Maria", "Naomi", "London", "Payton", "Lydia", "Ellie", "Mariah", "Aubree", "Kaitlyn", 
            "Violet", "Rylee", "Lilly", "Angelina", "Katelyn", "Mya", "Paige", "Natalia", "Ruby", "Piper", 
            "Annabelle", "Mary", "Jade", "Isabelle", "Liliana", "Nicole", "Rachel", "Vanessa", "Gabrielle", "Jessica", 
            "Jordyn", "Reagan", "Kendall", "Sadie", "Valeria", "Brielle", "Lyla", "Isabel", "Brooklynn", "Reese", 
            "Sara", "Adriana", "Aliyah", "Jennifer", "Mckenzie", "Gracie", "Nora", "Kylee", "Makenzie", "Izabella", 
            "Laila", "Alice", "Amy", "Michelle", "Skylar", "Stephanie", "Juliana", "Rebecca", "Jayla", "Eleanor", 
            "Clara", "Giselle", "Valentina", "Vivian", "Alaina", "Eliana", "Aria", "Valerie", "Haley", "Elena", 
            "Catherine", "Elise", "Lila", "Megan", "Gabriela", "Daisy", "Jada", "Daniela", "Penelope", "Jenna", 
            "Ashlyn", "Delilah", "Summer", "Mila", "Kate", "Keira", "Adrianna", "Hadley", "Julianna", "Maci", 
            "Eden", "Josephine", "Aurora", "Melissa", "Hayden", "Alana", "Margaret", "Quinn", "Angela", "Brynn", 
            "Alivia", "Katie", "Ryleigh", "Kinley", "Paisley", "Jordan", "Aniyah", "Allie", "Miranda", "Jacqueline", 
            "Melody", "Willow", "Diana", "Cora", "Alexandria", "Mikayla", "Danielle", "Londyn", "Addyson", "Amaya", 
            "Hazel", "Callie", "Teagan", "Adalyn", "Ximena", "Angel", "Kinsley", "Shelby", "Makenna", "Ariel", 
            "Jillian", "Chelsea", "Alayna", "Harmony", "Sienna", "Amanda", "Presley", "Maggie", "Tessa", "Leila", 
            "Hope", "Genevieve", "Erin", "Briana", "Delaney", "Esther", "Kathryn", "Ana", "Mckenna", "Camille", 
            "Cecilia", "Lucia", "Lola", "Leilani", "Leslie", "Ashlynn", "Kayleigh", "Alondra", "Alison", "Haylee", 
            "Carly", "Juliet", "Lexi", "Kelsey", "Eliza", "Josie", "Marissa", "Marley", "Alicia", "Amber", 
            "Sabrina", "Kaydence", "Norah", "Allyson", "Alina", "Ivy", "Fiona", "Isla", "Nadia", "Kyleigh", 
            "Christina", "Emery", "Laura", "Cheyenne", "Alexia", "Emerson", "Sierra", "Luna", "Cadence", "Daniella", 
            "Fatima", "Bianca", "Cassidy", "Veronica", "Kyla", "Evangeline", "Karen", "Adeline", "Jazmine", "Mallory", 
            "Rose", "Jayden", "Kendra", "Camryn", "Macy", "Abby", "Dakota", "Mariana", "Gia", "Adelyn", 
            "Madilyn", "Jazmin", "Iris", "Nina", "Georgia", "Lilah", "Breanna", "Kenzie", "Jayda", "Phoebe", 
            "Lilliana", "Kamryn", "Athena", "Malia", "Nyla", "Miley", "Heaven", "Audrina", "Madeleine", "Kiara", 
            "Selena", "Maddison", "Giuliana", "Emilia", "Lyric", "Joanna", "Adalynn", "Annabella", "Fernanda", "Aubrie", 
            "Heidi", "Esmeralda", "Kira", "Elliana", "Arabella", "Kelly", "Karina", "Paris", "Caitlyn", "Kara", 
            "Raegan", "Miriam", "Crystal", "Alejandra", "Tatum", "Savanna", "Tiffany", "Ayla", "Carmen", "Maliyah", 
            "Karla", "Bethany", "Guadalupe", "Kailey", "Macie", "Gemma", "Noelle", "Rylie", "Elaina", "Lena", 
            "Amiyah", "Ruth", "Ainsley", "Finley", "Danna", "Parker", "Emely", "Jane", "Joselyn", "Scarlet", 
            "Anastasia", "Journey", "Angelica", "Sasha", "Yaretzi", "Charlie", "Juliette", "Lia", "Brynlee", "Angelique", 
            "Katelynn", "Nayeli", "Vivienne", "Addisyn", "Kaelyn", "Annie", "Tiana", "Kyra", "Janelle", "Cali", 
            "Aleah", "Caitlin", "Imani", "Jayleen", "April", "Julie", "Alessandra", "Julissa", "Kailyn", "Jazlyn", 
            "Janiyah", "Kaylie", "Madelynn", "Baylee", "Itzel", "Monica", "Adelaide", "Brylee", "Michaela", "Madisyn", 
            "Cassandra", "Elle", "Kaylin", "Aniya", "Dulce", "Olive", "Jaelyn", "Courtney", "Brittany", "Madalyn", 
            "Jasmin", "Kamila", "Kiley", "Tenley", "Braelyn", "Holly", "Helen", "Hayley", "Carolina", "Cynthia", 
            "Talia", "Anya", "Estrella", "Bristol", "Jimena", "Harley", "Jamie", "Rebekah", "Charlee", "Lacey", 
            "Jaliyah", "Cameron", "Sarai", "Caylee", "Kennedi", "Dayana", "Tatiana", "Serena", "Eloise", "Daphne", 
            "Mckinley", "Mikaela", "Celeste", "Hanna", "Lucille", "Skyler", "Nylah", "Camilla", "Lilian", "Lindsey", 
            "Sage", "Viviana", "Danica", "Liana", "Melany", "Aileen", "Lillie", "Kadence", "Zariah", "June", 
            "Lilyana", "Bridget", "Anabelle", "Lexie", "Anaya", "Skye", "Alyson", "Angie", "Paola", "Elsie", 
            "Erica", "Gracelyn", "Kiera", "Myla", "Aylin", "Lana", "Priscilla", "Kassidy", "Natasha", "Nia", 
            "Kenley", "Dylan", "Kali", "Ada", "Miracle", "Raelynn", "Briella", "Emilee", "Lorelei", "Francesca", 
            "Arielle", "Madyson", "Amira", "Jaelynn", "Nataly", "Annika", "Joy", "Alanna", "Shayla", "Brenna", 
            "Sloane", "Vera", "Abbigail", "Amari", "Jaycee", "Lauryn", "Skyla", "Whitney", "Aspen", "Johanna", 
            "Jaylah", "Nathalie", "Laney", "Logan", "Brinley", "Leighton", "Marlee", "Ciara", "Justice", "Brenda", 
            "Kayden", "Erika", "Elisa", "Lainey", "Rowan", "Annabel", "Teresa", "Dahlia", "Janiya", "Lizbeth", 
            "Nancy", "Aleena", "Kaliyah", "Farrah", "Marilyn", "Eve", "Anahi", "Rosalie", "Jaylynn", "Bailee", 
            "Emmalyn", "Madilynn", "Lea", "Sylvia", "Annalise", "Averie", "Yareli", "Zoie", "Samara", "Amani", 
            "Regina", "Hailee", "Arely", "Evelynn", "Luciana", "Natalee", "Anika", "Liberty", "Giana", "Haven", 
            "Gloria", "Gwendolyn", "Jazlynn", "Marisol", "Ryan", "Virginia", "Myah", "Elsa", "Selah", "Melina", 
            "Aryanna", "Adelynn", "Raelyn", "Miah", "Sariah", "Kaylynn", "Amara", "Helena", "Jaylee", "Maeve", 
            "Raven", "Linda", "Anne", "Desiree", "Madalynn", "Meredith", "Clarissa", "Elyse", "Marie", "Alissa", 
            "Anabella", "Hallie", "Denise", "Elisabeth", "Kaia", "Danika", "Kimora", "Milan", "Claudia", "Dana", 
            "Siena", "Zion", "Ansley", "Sandra", "Cara", "Halle", "Maleah", "Marina", "Saniyah", "Casey", 
            "Harlow", "Kassandra", "Charley", "Rosa", "Shiloh", "Tori", "Adele", "Kiana", "Ariella", "Jaylene", 
            "Joslyn", "Kathleen", "Aisha", "Amya", "Ayanna", "Isis", "Karlee", "Cindy", "Perla", "Janessa", 
            "Lylah", "Raquel", "Zara", "Evie", "Phoenix", "Catalina", "Lilianna", "Mollie", "Simone", "Briley", 
            "Bria", "Kristina", "Lindsay", "Rosemary", "Cecelia", "Kourtney", "Aliya", "Asia", "Elin", "Isabela", 
            "Kristen", "Yasmin", "Alani", "Aiyana", "Amiya", "Felicity", "Patricia", "Kailee", "Adrienne", "Aliana", 
            "Ember", "Mariyah", "Mariam", "Ally", "Bryanna", "Tabitha", "Wendy", "Sidney", "Clare", "Aimee", 
            "Laylah", "Maia", "Karsyn", "Greta", "Noemi", "Jayde", "Kallie", "Leanna", "Irene", "Jessie", 
            "Paityn", "Kaleigh", "Lesly", "Gracelynn", "Amelie", "Iliana", "Elaine", "Lillianna", "Ellen", "Taryn", 
            "Lailah", "Rylan", "Lisa", "Emersyn", "Braelynn", "Shannon", "Beatrice", "Heather", "Jaylin", "Taliyah", 
            "Arya", "Emilie", "Ali", "Janae", "Chaya", "Cherish", "Jaida", "Journee", "Sawyer", "Destinee", 
            "Emmalee", "Ivanna", "Charli", "Jocelynn", "Kaya", "Elianna", "Armani", "Kaitlynn", "Rihanna", "Reyna", 
            "Christine", "Alia", "Leyla", "Mckayla", "Celia", "Raina", "Alayah", "Macey", "Meghan", "Zaniyah", 
            "Carolyn", "Kynlee", "Carlee", "Alena", "Bryn", "Jolie", "Carla", "Eileen", "Keyla", "Saniya", 
            "Livia", "Amina", "Angeline", "Krystal", "Zaria", "Emelia", "Renata", "Mercedes", "Paulina", "Diamond", 
            "Jenny", "Aviana", "Ayleen", "Barbara", "Alisha", "Jaqueline", "Maryam", "Julianne", "Matilda", "Sonia", 
            "Edith", "Martha", "Audriana", "Kaylyn", "Emmy", "Giada", "Tegan", "Charleigh", "Haleigh", "Nathaly", 
            "Susan", "Kendal", "Leia", "Jordynn", "Amirah", "Giovanna", "Mira", "Addilyn", "Frances", "Kaitlin", 
            "Kyndall", "Myra", "Abbie", "Samiyah", "Taraji", "Braylee", "Corinne", "Jazmyn", "Kaiya", "Lorelai", 
            "Abril", "Kenya", "Mae", "Hadassah", "Alisson", "Haylie", "Brisa", "Deborah", "Mina", "Rayne", 
            "America", "Ryann", "Milania", "Pearl", "Blake", "Millie", "Deanna", "Araceli", "Demi", "Gisselle", 
            "Paula", "Karissa", "Sharon", "Kensley", "Rachael", "Aryana", "Chanel", "Natalya", "Hayleigh", "Paloma", 
            "Avianna", "Jemma", "Moriah", "Renee", "Alyvia", "Zariyah", "Hana", "Judith", "Kinsey", "Salma", 
            "Kenna", "Mara", "Patience", "Saanvi", "Cristina", "Dixie", "Kaylen", "Averi", "Carlie", "Kirsten", 
            "Lilyanna", "Charity", "Larissa", "Zuri", "Chana", "Ingrid", "Lina", "Tianna", "Lilia", "Marisa", 
            "Nahla", "Sherlyn", "Adyson", "Cailyn", "Princess", "Yoselin", "Aubrianna", "Maritza", "Rayna", "Luz", 
            "Cheyanne", "Azaria", "Jacey", "Roselyn", "Elliot", "Jaiden", "Tara", "Alma", "Esperanza", "Jakayla", 
            "Yesenia", "Kiersten", "Marlene", "Nova", "Adelina", "Ayana", "Kai", "Nola", "Sloan", "Avah", 
            "Carley", "Meadow", "Neveah", "Tamia", "Alaya", "Jadyn", "Sanaa", "Kailynn", "Diya", "Rory", 
            "Abbey", "Karis", "Maliah", "Belen", "Bentley", "Jaidyn", "Shania", "Britney", "Yazmin", "Aubri", 
            "Malaya", "Micah", "River", "Alannah", "Jolene", "Shaniya", "Tia", "Yamilet", "Bryleigh", "Carissa", 
            "Karlie", "Libby", "Lilith", "Lara", "Tess", "Aliza", "Laurel", "Kaelynn", "Leona", "Regan", 
            "Yaritza", "Kasey", "Mattie", "Audrianna", "Blakely", "Campbell", "Dorothy", "Julieta", "Kylah", "Kyndal", 
            "Temperance", "Tinley", "Akira", "Saige", "Ashtyn", "Jewel", "Kelsie", "Miya", "Cambria", "Analia", 
            "Janet", "Kairi", "Aleigha", "Bree", "Dalia", "Liv", "Sarahi", "Yamileth", "Carleigh", "Geraldine", 
            "Izabelle", "Riya", "Samiya", "Abrielle", "Annabell", "Leigha", "Pamela", "Caydence", "Joyce", "Juniper", 
            "Malaysia", "Isabell", "Blair", "Jaylyn", "Marianna", "Rivka", "Alianna", "Gwyneth", "Kendyl", "Sky", 
            "Esme", "Jaden", "Sariyah", "Stacy", "Kimber", "Kamille", "Milagros", "Karly", "Karma", "Thalia", 
            "Willa", "Amalia", "Hattie", "Payten", "Anabel", "Ann", "Galilea", "Milana", "Yuliana", "Damaris",
        };
        #endregion
    }
}
