import React, { useState, useEffect } from 'react';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import PdfConversionItem from './PdfConversionItem';
import { PdfConversionModel } from '../types/PdfConversionModel';

const PdfConversionList: React.FC = () => {
    const [conversions, setConversions] = useState<PdfConversionModel[]>([]);
    const [error, setError] = useState<string | null>(null);

    const fetchConversions = async () => {
        try {
            const response = await fetch('http://localhost:5000/PdfConversion');
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data: PdfConversionModel[] = await response.json();
            setConversions(data);
        } catch (error: any) {
            setError(error.message);
        }
    };

    useEffect(() => {
        fetchConversions(); // Выполняем первоначальную загрузку списка конверсий
    }, []);

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('http://localhost:5000/pdf-conversion-hub')
            .withAutomaticReconnect()
            .build();

        newConnection.start()
            .then(() => {
                console.log('Connection to hub established.');
                newConnection.on('ConversionsUpdated', fetchConversions);
            })
            .catch(e => console.log('Connection failed: ', e));

        return () => {
            newConnection.stop();
        };
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
