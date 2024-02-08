export interface PdfConversionModel {
    id: string;
    status: ConversionStatus;
    originFileName: string;
    originFilePath: string;
    resultPath?: string;
    errorMessage?: string;
}

export enum ConversionStatus {
    InProcess = 1,
    Completed = 2,
    Error = 3
}