export function ImageUploadForm() {
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
          <button style={{ padding: '2px 14px', backgroundColor: '#007bff', color: '#fff', border: 'none', borderRadius: '4px', cursor: 'pointer' }}>
            Browse
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
          <input type="text" style={{ width: '100%', boxSizing: 'border-box' }} name="title" />

          <span>Description:</span>
          <input type="text" style={{ width: '100%', boxSizing: 'border-box' }} name="description" />

          <span>Category:</span>
          <select name="category" style={{ width: '100%', boxSizing: 'border-box' }}>
            <option value="profile-picture">Profile picture</option>
            <option value="inspiration">Inspiration</option>
          </select>
        </section>
      </section>
    )
}