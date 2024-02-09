import React from 'react';
import './App.css';
import PdfConversionList from './containers/pdf-conversion-list/PdfConversionList'; // Путь к вашему компоненту List
import FileUploadComponent from './components/file-upload/FileUploadComponent'; // Путь к вашему компоненту List

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