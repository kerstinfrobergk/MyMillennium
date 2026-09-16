import { uploadImage } from "../services/artService";
import { useState } from "react";


export function ImageUploadForm() {
    const [imageFile, setImageFile] = useState(null);
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [category, setCategory] = useState(1);

    return (
      <section
        style={{
          padding: '10px',
          minWidth: '400px',
          maxWidth: '500px',
          margin: '8px auto',
          background: '#e1dfdf'
        }} >
        <div style={{ marginBottom: '15px' }}>
          <span> Choose a file to upload: </span>
          <button style={{ padding: '2px 14px', marginRight: '10px', backgroundColor: '#007bff', color: '#fff', border: 'none', borderRadius: '4px', cursor: 'pointer' }}>
            browse
          </button>
          <button
            style={{ padding: '2px 14 px', backgroundColor: 'violet', color: '#fff', borderRadius: '4px', cursor: 'pointer' }}

            // onClick={() =>
            //     uploadImage(
            //         "fdsildf",
            //         "dsfsdf",
            //         "dsfs",
            //         0
            //     ) }
            >
            upload
          </button>
        </div>

        <section id="file-info"
          style={{
            display: 'grid',
            gridTemplateColumns: '25% 65%',
            rowGap: '15px',
            alignItems: 'center',
            justifyItems: 'start',
          }} >
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
            <option value={1}>Profile picture</option>
            <option value={2}>Inspiration</option>

          </select>
        </section>
      </section>
    )
}