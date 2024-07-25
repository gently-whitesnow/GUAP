export const setFile = (file) => {
    if (!file) return undefined;
    
    const fileData = atob(file);
    const fileByteArray = new Uint8Array(fileData.length);
    
    for (let i = 0; i < fileData.length; i++) {
      fileByteArray[i] = fileData.charCodeAt(i);
    }
    
    const fileBlob = new Blob([fileByteArray], { type: "application/octet-stream" });
    const fileURL = URL.createObjectURL(fileBlob);
    
    return fileURL;
  };