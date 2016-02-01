using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBLA_Members {
    public class StatesArray {

        public static List<US_State> states;
        public static Dictionary<String, String> AbbrevToState = new Dictionary<String, String>();

        public static void PopulateArray () {
            states = new List<US_State>(50);
            states.Add(new US_State("AL", "Alabama"));
            states.Add(new US_State("AK", "Alaska"));
            states.Add(new US_State("AZ", "Arizona"));
            states.Add(new US_State("AR", "Arkansas"));
            states.Add(new US_State("CA", "California"));
            states.Add(new US_State("CO", "Colorado"));
            states.Add(new US_State("CT", "Connecticut"));
            states.Add(new US_State("DE", "Delaware"));
            states.Add(new US_State("DC", "District Of Columbia"));
            states.Add(new US_State("FL", "Florida"));
            states.Add(new US_State("GA", "Georgia"));
            states.Add(new US_State("HI", "Hawaii"));
            states.Add(new US_State("ID", "Idaho"));
            states.Add(new US_State("IL", "Illinois"));
            states.Add(new US_State("IN", "Indiana"));
            states.Add(new US_State("IA", "Iowa"));
            states.Add(new US_State("KS", "Kansas"));
            states.Add(new US_State("KY", "Kentucky"));
            states.Add(new US_State("LA", "Louisiana"));
            states.Add(new US_State("ME", "Maine"));
            states.Add(new US_State("MD", "Maryland"));
            states.Add(new US_State("MA", "Massachusetts"));
            states.Add(new US_State("MI", "Michigan"));
            states.Add(new US_State("MN", "Minnesota"));
            states.Add(new US_State("MS", "Mississippi"));
            states.Add(new US_State("MO", "Missouri"));
            states.Add(new US_State("MT", "Montana"));
            states.Add(new US_State("NE", "Nebraska"));
            states.Add(new US_State("NV", "Nevada"));
            states.Add(new US_State("NH", "New Hampshire"));
            states.Add(new US_State("NJ", "New Jersey"));
            states.Add(new US_State("NM", "New Mexico"));
            states.Add(new US_State("NY", "New York"));
            states.Add(new US_State("NC", "North Carolina"));
            states.Add(new US_State("ND", "North Dakota"));
            states.Add(new US_State("OH", "Ohio"));
            states.Add(new US_State("OK", "Oklahoma"));
            states.Add(new US_State("OR", "Oregon"));
            states.Add(new US_State("PA", "Pennsylvania"));
            states.Add(new US_State("RI", "Rhode Island"));
            states.Add(new US_State("SC", "South Carolina"));
            states.Add(new US_State("SD", "South Dakota"));
            states.Add(new US_State("TN", "Tennessee"));
            states.Add(new US_State("TX", "Texas"));
            states.Add(new US_State("UT", "Utah"));
            states.Add(new US_State("VT", "Vermont"));
            states.Add(new US_State("VA", "Virginia"));
            states.Add(new US_State("WA", "Washington"));
            states.Add(new US_State("WV", "West Virginia"));
            states.Add(new US_State("WI", "Wisconsin"));
            states.Add(new US_State("WY", "Wyoming"));

            AbbrevToState.Add("AL", "Alabama");
            AbbrevToState.Add("AK", "Alaska");
            AbbrevToState.Add("AZ", "Arizona");
            AbbrevToState.Add("AR", "Arkansas");
            AbbrevToState.Add("CA", "California");
            AbbrevToState.Add("CO", "Colorado");
            AbbrevToState.Add("CT", "Connecticut");
            AbbrevToState.Add("DE", "Delaware");
            AbbrevToState.Add("DC", "District of Columbia");
            AbbrevToState.Add("FL", "Florida");
            AbbrevToState.Add("GA", "Georgia");
            AbbrevToState.Add("HI", "Hawaii");
            AbbrevToState.Add("ID", "Idaho");
            AbbrevToState.Add("IL", "Illinois");
            AbbrevToState.Add("IN", "Indiana");
            AbbrevToState.Add("IA", "Iowa");
            AbbrevToState.Add("KS", "Kansas");
            AbbrevToState.Add("KY", "Kentucky");
            AbbrevToState.Add("LA", "Louisiana");
            AbbrevToState.Add("ME", "Maine");
            AbbrevToState.Add("MD", "Maryland");
            AbbrevToState.Add("MA", "Massachusetts");
            AbbrevToState.Add("MI", "Michigan");
            AbbrevToState.Add("MN", "Minnesota");
            AbbrevToState.Add("MS", "Mississippi");
            AbbrevToState.Add("MO", "Missouri");
            AbbrevToState.Add("MT", "Montana");
            AbbrevToState.Add("NE", "Nebraska");
            AbbrevToState.Add("NV", "Nevada");
            AbbrevToState.Add("NH", "New Hampshire");
            AbbrevToState.Add("NJ", "New Jersey");
            AbbrevToState.Add("NM", "New Mexico");
            AbbrevToState.Add("NY", "New York");
            AbbrevToState.Add("NC", "North Carolina");
            AbbrevToState.Add("ND", "North Dakota");
            AbbrevToState.Add("OH", "Ohio");
            AbbrevToState.Add("OK", "Oklahoma");
            AbbrevToState.Add("OR", "Oregon");
            AbbrevToState.Add("PA", "Pennsylvania");
            AbbrevToState.Add("RI", "Rhode Island");
            AbbrevToState.Add("SC", "South Carolina");
            AbbrevToState.Add("SD", "South Dakota");
            AbbrevToState.Add("TN", "Tennessee");
            AbbrevToState.Add("TX", "Texas");
            AbbrevToState.Add("UT", "Utah");
            AbbrevToState.Add("VT", "Vermont");
            AbbrevToState.Add("VA", "Virginia");
            AbbrevToState.Add("WA", "Washington");
            AbbrevToState.Add("WV", "West Virginia");
            AbbrevToState.Add("WI", "Wisconsin");
            AbbrevToState.Add("WY", "Wyoming");
        }

        public static string[] Abbreviations () {
            List<string> abbrevList = new List<string>(states.Count);
            foreach (var state in states) {
                abbrevList.Add(state.Abbreviations);
            }
            return abbrevList.ToArray();
        }

        public static string[] Names () {
            List<string> nameList = new List<string>(states.Count);
            foreach (var state in states) {
                nameList.Add(state.Name);
            }
            return nameList.ToArray();
        }

        public static US_State[] States () {
            return states.ToArray();
        }

    }

    public class US_State {

        public US_State () {
            Name = null;
            Abbreviations = null;
        }

        public US_State (string ab, string name) {
            Name = name;
            Abbreviations = ab;
        }

        public string Name { get; set; }

        public string Abbreviations { get; set; }

        public override string ToString () {
            return string.Format("{0} - {1}", Abbreviations, Name);
        }

    }
}
