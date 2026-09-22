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
      <section className="image-upload-form">
        <div className="file-upload" >
            <span className="file-upload-label" >
                Upload image from computer: </span>
            <span className="file-name" >
                {imageFile ? imageFile.name : "No file selected"}
            </span>

            <button
                onClick={() => fileInputRef.current?.click()}
                className="browse-button" >
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

        <section className="file-info">
          <span>Title:</span>
          <input
            type="text"
            name="title"
            value={title}
            onChange={(event) => setTitle(event.target.value)} />
          <span>Description:</span>
          <input type="text"
            name="description"
            value={description}
            onChange={(event) => setDescription(event.target.value)} />
          <span>Category:</span>
          <select name="category"
            value={category}
            onChange={(event) => setCategory(Number(event.target.value))} >
            <option value={0}>Inspiration</option>
            <option value={1}>Profile picture</option>
          </select>
        </section>
        <br></br>
        <button className="upload-button"
            onClick={handleUpload} >
            upload
        </button>
      </section>
    )
}