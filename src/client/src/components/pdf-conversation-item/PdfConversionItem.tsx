import React from 'react';
import { PdfConversionModel, ConversionStatus } from '../../types/PdfConversionModel';
import styles from './PdfConversionItem.module.css'; 

interface PdfConversionItemProps {
    conversion: PdfConversionModel;
}

const PdfConversionItem: React.FC<PdfConversionItemProps> = ({ conversion }) => {
    const getStatusClassName = (status: ConversionStatus) => {
        switch (status) {
            case ConversionStatus.Completed:
                return styles.statusCompleted;
            case ConversionStatus.InProcess:
                return styles.statusInProcess;
            default:
                return '';
        }
    };

    const downloadUrl = `http://localhost:8080/pdfConversion/download-conversion/${conversion.resultPath}`;

    return (
        <div className={styles.conversionItem}>
            <h3 className={styles.fileName}>{conversion.originFileName}</h3>
            <span className={`${styles.statusBadge} ${getStatusClassName(conversion.status)}`}>
                {ConversionStatus[conversion.status]}
            </span>
            {conversion.resultPath && (
                <div className={styles.resultLinkContainer}>
                    <a href={downloadUrl} className={styles.resultLink} download>Download</a>
                </div>
            )}
        </div>
    );
};

export default PdfConversionItem;
