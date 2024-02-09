import React, {useState} from 'react';
import styles from './FileUploadComponent.module.css';

function FileUploadComponent() {
    const [selectedFile, setSelectedFile] = useState(null);
    const [fileName, setFileName] = useState('No files selected');

    const handleFileSelect = (event:any) => {
        setSelectedFile(event.target.files[0]);
        setFileName(event.target.files[0] ? event.target.files[0].name : 'No files selected');
    };

    const handleUpload = async () => {
        if (!selectedFile) {
            alert('Please select a file first!');
            return;
        }

        const formData = new FormData();
        formData.append('htmlContent', selectedFile);

        try {
            await fetch('http://localhost:8080/PdfConversion/queue-conversion', {
                method: 'POST',
                body: formData,
            });
        } catch (error) {
            console.error('Error during the upload', error);
            alert('File upload failed');
        }
    };

    return (
        <div>
            <label className={styles.fileInputLabel}>
                {fileName}
                <input
                    type="file"
                    onChange={handleFileSelect}
                    className={styles.fileInput}
                    accept=".html"
                />
            </label>
            <button onClick={handleUpload} className={styles.uploadButton}>
                Upload
            </button>
        </div>
    );
}

export default FileUploadComponent;
