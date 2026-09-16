import { uploadImage } from "../services/artService";
import { useRef, useState } from "react";


export function ImageUploadForm() {
    const [imageFile, setImageFile] = useState<File | null>(null);
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [category, setCategory] = useState(0);

    const fileInputRef = useRef<HTMLInputElement>(null);

    async function handleUpload(){
        if (!imageFile){
            return;
        }

        await uploadImage(
            imageFile,
            title,
            description,
            category
        )
    }

    return (
      <section
        style={{
          padding: '10px',
          minWidth: '400px',
          maxWidth: '500px',
          margin: '8px auto',
          background: '#e1dfdf'
        }} >
        <div
            style={{
                marginBottom: '15px',
                display: "grid",
                gridTemplateColumns: "auto 1fr auto",
                gap: "10px",
                alignItems: "center"
            }}
        >
            <span style={{ whiteSpace: "nowrap"}} >
                Upload image from computer: </span>
            <span
                style={{
                    border: "1px solid #888",
                    background: "white",
                    padding: "3px 8px",
                    minWidth: "180px",
                    textAlign: "left",
                    overflow: "hidden",
                    textOverflow: "ellipsis",
                    whiteSpace: "nowrap"
                }}
            >
                {imageFile ? imageFile.name : "No file selected"}
            </span>

            <button onClick={() => fileInputRef.current?.click()}
                style={{
                    padding: '2px 14px',
                    marginRight: '10px',
                    backgroundColor: '#007bff',
                    color: '#fff',
                    borderRadius: '4px',
                    cursor: 'pointer' }}>
                browse
            </button>

            <input
                ref={fileInputRef}
                type="file"
                accept="image/*"
                hidden
                onChange={(event) => {
                    const file = event.target.files?.[0];

                    if (file) {
                        setImageFile(file);
                    }
                }}
            >
            </input>
        </div>

        <section id="file-info"
          style={{
            display: 'grid',
            gridTemplateColumns: '25% 65%',
            rowGap: '15px',
            alignItems: 'center',
            justifyItems: 'start',
          }}
        >
          <span>Title:</span>
          <input type="text"
            style={{ width: '100%', boxSizing: 'border-box' }}
            name="title"
            value={title}
            onChange={(event) => setTitle(event.target.value)} />
          <span>Description:</span>
          <input type="text"
            style={{ width: '100%', boxSizing: 'border-box' }}
            name="description"
            value={description}
            onChange={(event) => setDescription(event.target.value)} />
          <span>Category:</span>
          <select name="category"
            style={{ width: '100%', boxSizing: 'border-box' }}
            value={category}
            onChange={(event) => setCategory(Number(event.target.value))} >
            <option value={0}>Inspiration</option>
            <option value={1}>Profile picture</option>
          </select>
        </section>

        <button
            onClick={handleUpload}
            style={{
                padding: '2px 14 px',
                backgroundColor: 'violet',
                color: '#fff',
                borderRadius: '4px',
                cursor: 'pointer' }}
        >
            upload
        </button>
      </section>
    )
}