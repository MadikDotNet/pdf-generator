// PdfConversionList.tsx
import React, { useState, useEffect } from 'react';
import PdfConversionItem from './PdfConversionItem';
import { PdfConversionModel } from '../types/PdfConversionModel';

const PdfConversionList: React.FC = () => {
    const [conversions, setConversions] = useState<PdfConversionModel[]>([]);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchConversions = async () => {
            try {
                const response: Response = await fetch('http://207.180.214.41:8080/PdfConversion');
                console.log(response.json())
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                const data: PdfConversionModel[] = await response.json();
                setConversions(data);
            } catch (error: any) {
                setError(error.message);
            }
        };

        fetchConversions();
    }, []);

    return (
        <div className="conversion-list">
            {error && <p className="error-message">Error: {error}</p>}
            {conversions.map((conversion) => (
                <PdfConversionItem key={conversion.id} conversion={conversion} />
            ))}
        </div>
    );
};

export default PdfConversionList;
