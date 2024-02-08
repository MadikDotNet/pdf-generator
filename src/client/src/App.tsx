import React from 'react';
import './App.css';
import PdfConversionList from './features/PdfConversions/components/PdfConversionList'; // Путь к вашему компоненту List
import FileUploadComponent from './features/PdfConversions/components/FileUploadComponent'; // Путь к вашему компоненту List

function App() {
    return (
        <div className="App">
            <main>
                <FileUploadComponent />
                <PdfConversionList/>
            </main>
        </div>
    );
}

export default App;