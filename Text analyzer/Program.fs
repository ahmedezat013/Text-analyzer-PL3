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

 if Seq.isEmpty wordFrequency then
        "No words found in the text."
 else
    let mostFrequentWord = Seq.head wordFrequency
    let averageSentenceLength = 
        if sentences.Length > 0 then 
            float words.Length / float sentences.Length 
        else 
            0.0
    let averageWordLength = 
        if words.Length > 0 then 
            float (words |> Seq.sumBy String.length) / float words.Length 
        else 
            0.0       
// Updated sprintf with proper argument wrapping
        sprintf """
        
Words: %d
Sentences: %d
Paragraphs: %d
Most Frequent Word: %s (%d occurrences)
Average Sentence Length: %.2f words
Average Word Length: %.2f characters



""" 
            words.Length 
            sentences.Length 
            paragraphs.Length 
            (fst mostFrequentWord) 
            (snd mostFrequentWord) 
            averageSentenceLength 
            averageWordLength

// Event handler for the Load File button
loadButton.Click.Add (fun _ ->
    let openFileDialog = new OpenFileDialog(Filter = "Text Files|*.txt")
    if openFileDialog.ShowDialog() = DialogResult.OK then
        let fileContent = File.ReadAllText(openFileDialog.FileName)
        textBox.Text <- fileContent
)
         
