import React, {useState} from 'react';
import styles from './FileUploadComponent.module.css';

function FileUploadComponent() {
    const [selectedFile, setSelectedFile] = useState(null);

    const handleFileSelect = (event: any) => {
        setSelectedFile(event.target.files[0]);
    };

    const handleUpload = async () => {
        if (!selectedFile) {
            alert('Please select a file first!');
            return;
        }

        const formData = new FormData();
        formData.append('file', selectedFile);

        try {
            const response = await fetch('http://207.180.214.41:8080/PdfConversion/queue-conversion', {
                method: 'POST',
                body: formData,
            });

            if (response.ok) {
                alert('File successfully uploaded');
            } else {
                alert('File upload failed');
            }
        } catch (error) {
            console.error('Error during the upload', error);
            alert('File upload failed');
        }
    };

    return (
        <div>
            <div className={styles.fileInputContainer}>
                <label className={styles.fileInput}>
                    <input type="file" onChange={handleFileSelect} style={{ display: 'none' }} />
                    Не выбран ни один файл
                </label>
                <button onClick={handleUpload} className={styles.uploadButton}>
                    Upload
                </button>
            </div>
            {/* ... остальная часть компонента ... */}
        </div>
    );
}

export default FileUploadComponent;
