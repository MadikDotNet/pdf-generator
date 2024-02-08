import React from 'react';
import { PdfConversionModel, ConversionStatus } from '../types/PdfConversionModel';

interface PdfConversionItemProps {
    conversion: PdfConversionModel;
}

const PdfConversionItem: React.FC<PdfConversionItemProps> = ({ conversion }) => {
    return (
        <div className="conversion-item">
            <h3>{conversion.originFileName}</h3>
            <p>Status: {ConversionStatus[conversion.status]}</p>
            {conversion.resultPath && <p>Result: <a href={conversion.resultPath}>Download</a></p>}
            {conversion.errorMessage && <p>Error: {conversion.errorMessage}</p>}
        </div>
    );
};

export default PdfConversionItem;