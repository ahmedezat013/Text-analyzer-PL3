open System
open System.Windows.Forms
open System.IO

// Create the main form
let form: Form = new Form(Text = "Text Analyzer", Width = 800, Height = 600)
// Create UI elements with explicit type annotations
let textBox: TextBox = new TextBox(Multiline = true, Width = 700, Height = 300, Top = 20, Left = 20)
let loadButton: Button = new Button(Text = "Load File", Width = 100, Top = 350, Left = 20)
let analyzeButton: Button = new Button(Text = "Analyze Text", Width = 120, Top = 350, Left = 140)
let resultsLabel: Label = new Label(Text = "Results will appear here.", Top = 400, Left = 20, Width = 700, Height = 170, AutoSize = false)
// Add the UI elements to the form
form.Controls.AddRange([| textBox :> Control; loadButton :> Control; analyzeButton :> Control; resultsLabel :> Control |])

// Function to analyze text
let analyzeText (text: string): string =
    let sentences = text.Split([| '.'; '!'; '?' |], StringSplitOptions.RemoveEmptyEntries)
    let paragraphs = text.Split([| '\n' |], StringSplitOptions.RemoveEmptyEntries)
    let words = text.Split([| ' '; '\t'; '\n'; '.'; ','; '!' |], StringSplitOptions.RemoveEmptyEntries)
    let wordFrequency =
        words
        |> Seq.groupBy id
        |> Seq.map (fun (word, occurrences) -> word, Seq.length occurrences)
        |> Seq.sortByDescending snd
