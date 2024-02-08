using PdfGenerator.Worker;

var worker = new PdfGeneratorWorker(args);

await worker.RunAsync();