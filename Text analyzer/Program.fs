open System
open System.Windows.Forms
open System.IO

// Create the main form
let form: Form = new Form(Text = "Text Analyzer", Width = 800, Height = 600)

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