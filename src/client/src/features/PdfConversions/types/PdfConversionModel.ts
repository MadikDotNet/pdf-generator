export interface PdfConversionModel {
    id: string;
    status: ConversionStatus;
    originFileName: string;
    originFilePath: string;
    resultPath?: string;
    createdAt?: Date;
}

export enum ConversionStatus {
    InProcess = 1,
    Completed = 2,
}