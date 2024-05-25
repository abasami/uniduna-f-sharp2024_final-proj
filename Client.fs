namespace MidPrjFuji

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.Sitelets

[<JavaScript>]
module Client =
    // The templates are loaded from the DOM, so you just can edit index.html
    // and refresh your browser, no need to recompile unless you add or remove holes.
    type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>

    // type for list
    type Entry =
        {
            RegisterdDate: string;
            CoffeeName: string;
            MyNote: string;
            Level: int;
        }
        static member Create regdate coffeename productionarea level =
            {
                RegisterdDate = regdate
                CoffeeName = coffeename
                MyNote = productionarea
                Level = level
            }

    [<SPAEntryPoint>]
    let Main () =
        

        let newEntryCoffeeName = Var.Create ""       // blank
        let newEntryMyNote = Var.Create ""      // blank
        let newEntryLevel = Var.Create 5 // level 0 is set
        let msg = Var.Create ""                // blank
        let cap = Var.Create "=== REGISTERED COFFEE ==="
        let recipe = Var.Create "Information about the coffee you selected will be displayed here."
        let result = Var.Create ""

        let allCoffee =
            ListModel.FromSeq [
                Entry.Create "" "Blue Mountain" "Most Favorite!" 5
                Entry.Create "" "Kona" "The second-time purchased" 5
                Entry.Create "" "Kilimanjaro" "" 4
                Entry.Create "" "Mocha" "" 4
                Entry.Create "" "Java coffee" "" 3
                Entry.Create "" "Toraja" "" 3
                Entry.Create "" "Mandheling" "" 3
            ]

        // coffee list but for the display list
        let Coffee =
            ListModel.FromSeq []
        // initial value is copied from above permanent book list
        allCoffee.Iter(fun t ->
            Entry.Create (t.RegisterdDate) (t.CoffeeName) (t.MyNote) (t.Level) 
            |> Coffee.Add )

        let row entry =
            IndexTemplate.Row()
                .RegisteredDate(entry.RegisterdDate)
                .CoffeeName(entry.CoffeeName)
                .MyNote(entry.MyNote)
                .Level(string entry.Level)
                .Doc()

        // display coffee list
        let data =
            Coffee.View.Doc(fun lm -> 
//                lm 
// Sort not needed.
                lm 
                |> Seq.sortBy (fun t -> 
                    t.CoffeeName
                ) 

                |> Seq.map row
                |> Doc.Concat
            )

        //  replace from index.html
        IndexTemplate.Main()
            .Caption(cap.View)
            .Data(data)
            .CoffeeName(newEntryCoffeeName)
            .MyNote(newEntryMyNote)
            .Level(newEntryLevel)
            .Msg(msg)
            .Recipe(recipe.View)
            .Result(result.View)


            //  Regisiter button
            .Reister(fun _ ->
                cap.Value <- "=== REGISTERED COFFEE ==="
                Coffee.Clear()
                // add the registered date
                let date = string System.DateTime.Now.Year + "/" + string System.DateTime.Now.Month + "/" + string System.DateTime.Now.Day
                Entry.Create (date) (newEntryCoffeeName.Value) (newEntryMyNote.Value) (newEntryLevel.Value)
                |> Coffee.Add
                allCoffee.Iter(fun t -> 
                    Entry.Create (t.RegisterdDate) (t.CoffeeName) (t.MyNote) (t.Level)
                    |> Coffee.Add 
                    )
                allCoffee.Clear()
                Coffee.Iter(fun t -> 
                    Entry.Create (t.RegisterdDate) (t.CoffeeName) (t.MyNote) (t.Level)
                    |> allCoffee.Add 
                    )
                
                // Display message.   registered.

                recipe.Value <-
                    if newEntryCoffeeName.Value = "Blue Mountain" then //case Blue Mountain
                        "BLUE MOUNTAIN has an outstanding aroma, a harmonious taste, a light texture, and a smooth finish."+
                        "It is said to be of the highest quality. A small percentage of the coffee produced in Jamaica is branded as Blue Mountain." +
                        "Among them, it is further ranked" + String.FromCharCode(10) +
                        "Enjoy the taste straight!"

                    elif newEntryCoffeeName.Value = "Java Coffee" then
                        "JAVA COFFEE is Mainly refers to Arabica coffee grown on the island of Java. Round and mild taste."+ String.FromCharCode(10) +
                        "Enjoy the taste Iced Coffee!"

                    elif newEntryCoffeeName.Value = "Kilimanjaro" then
                        "KILIMANJARO is a name for coffee produced in Tanzania.  Characterized by strong acidity and richness." + 
                        "Deep roasts, which are often described as 'full of wildness', have an elegant bitterness " + 
                        "that is different from light to medium roasts."

                    elif newEntryCoffeeName.Value = "Kona" then
                        "KONA has a very strong sourness and rich flavor.It is said to give a good acidity when used in blends, and is the second most expensive brand after Blue Mountain.."

 
                    elif newEntryCoffeeName.Value = "Mandheling" then
                        "MANDHEILING comes from Sumatra. The taste is mainly bitter and full-bodied, with no acidity and a unique aftertaste."
  
                    elif newEntryCoffeeName.Value = "Mocha" then
                        "MOCHA has an excellent aroma and a unique sourness, adding sweetness and richness." + 
                        "It is the oldest brand. It is the origin of coffee, and in Italy and other countries coffee is called mocha." + 
                        "Famous include ``Matali'' from Yemen, ``Harar'' and ``Sidamo'' from Ethiopia."
  
                    elif newEntryCoffeeName.Value = "Toraja" then
                        "TORAJA comes from Sulawesi. The taste is mainly bitter, with a very rich flavor and no sourness.."

                    else
                        "Cofee \"" + string newEntryCoffeeName.Value + "\" from \"" + newEntryMyNote.Value + 
                        "\" is registerd."
 
                // result shows
                result.Value <- ("As of " + date + " , the number of lists you have registered is " + (string Coffee.Length) + "." )

                // Display message.   registered.
                msg.Value <- ("Coffee \"" + (string newEntryCoffeeName.Value) + "\" Note: \"" + (string newEntryMyNote.Value) + "\" Level: \"" + 
                (string newEntryLevel.Value)+ "\" is added to the LIST.")
                newEntryCoffeeName.Value <- ""   // clear only the title.  available same author quick entry.
                newEntryMyNote.Value <- ""   // clear only the title.  available same author quick entry.
                newEntryLevel.Value <- 5   // clear only the title.  available same author quick entry.

                )
               

            //  Search button

            .Search(fun _ ->
                cap.Value <- "=== MY SEARCH RESULT ==="
                Coffee.Clear()
                recipe.Value <- ""
                result.Value <- ""

                //  copy from the permanent coffee list, if the string contains

                allCoffee.Iter(fun t ->
                    let bCoffeeName:bool  = if  (newEntryCoffeeName.Value.Trim().Equals("")) then true else (t.CoffeeName.ToLower().Contains(newEntryCoffeeName.Value.ToLower()))
                    let bMyNote:bool = if  (newEntryMyNote.Value.Trim().Equals("")) then true else (t.MyNote.ToLower().Contains(newEntryMyNote.Value.ToLower()))
                    let clevel = string t.Level
                    let bLevel:bool = if  (newEntryLevel.Value.Equals("")) then true else (clevel.Contains(newEntryLevel.Value.ToString()))
                    if bCoffeeName && bMyNote && bLevel then
                        Entry.Create (t.RegisterdDate) (t.CoffeeName) (t.MyNote) (t.Level)
                        |> Coffee.Add
                    )
                // Display message.   found.
                msg.Value <-
                    if (Coffee.Length = 0) then
                        Entry.Create ("") ("") ("") (0)
                        |> Coffee.Add
                        "Coffee \"" + newEntryCoffeeName.Value + "\" Note: \"" + newEntryMyNote.Value + "\" is not found."
                    else
                        "Coffee \"" + newEntryCoffeeName.Value + "\" Note: \"" + newEntryMyNote.Value + "\" is found, " + (string Coffee.Length) + 
                            (if Coffee.Length = 1 then " Coffee." else " Coffee.")                  
                )
            .Doc()

        |> Doc.RunById "main"