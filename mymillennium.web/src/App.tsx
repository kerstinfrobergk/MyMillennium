import { useState } from 'react'
import { ImageUploadForm } from './components/ImageUploadForm'
// import './App.css'
import './styles/areas.css'
import { ImageGallery } from './components/ImageGallery'


function App() {
  const [count, setCount] = useState(0)

  return (
    <>
    <div className="wrapper">
      <header className="header">This page is up!</header>
      <main className='content'>
        
        <section id='introduction'>
          <div>
            <h1>Welcome!</h1>
          </div>
          <section id="spacer"/>
          <div>
            <h2> Some introduction text... </h2>
            <img className="base" width="170" height="80" alt="" />
          </div>
        </section>
        <br/>
        <section id='upload-image'>
          <ImageUploadForm/>
        </section>
        <section id='gallery'>
          <ImageGallery/>
        </section>
      </main>

    </div>
      <section id="center">
      </section>
      

      {/* <section
        style={{
          padding: '10px',
          minWidth: '380px',
          maxWidth: '400px',
          margin: '8px auto',
          background: '#e1dfdf'}} >
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
      </section> */}
    </>
  )
}
export default App
