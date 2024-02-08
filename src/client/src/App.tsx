import React from 'react';
import './App.css';
import PdfConversionList from './features/PdfConversions/components/PdfConversionList'; // Путь к вашему компоненту List

function App() {
  return (
      <div className="App">
        <header className="App-header">
          <h1>Conversions</h1>
        </header>
        <main>
          <PdfConversionList />
        </main>
      </div>
  );
}

export default App;