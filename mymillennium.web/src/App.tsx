import { useState } from 'react'
import React from 'react'
import { ImageUploadForm } from './components/ImageUploadForm'
import './App.css'
import { ImageGallery } from './components/ImageGallery'


function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <section id="center">
        <div>
          <h1>This page is up!</h1>
        </div>
      </section>
      <section id="spacer"></section>
      <section id="introduction">
        <div className="hero">
          <h2> Some introduction text... </h2>
          <img className="base" width="170" height="80" alt="" />
        </div>
      </section>
      <br/>
      <ImageUploadForm/>
      <ImageGallery/>
      
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
<br></br>
      <section>
        <div>
          <div id="song-info">
            <h3> Playing: The killers --- </h3>
          </div>
          <span>
            <button className="buttonPlay"
              type="button"
              onClick={() => "doSomething"}
            >
              play
            </button>
            <text> </text>
            <button className="buttonStop"
              type="button"
              onClick={() => "doSomething"}
            >
              stop
            </button> 
          </span>
          <br/>
          <br/>
        </div>
      </section>
      <section id="spacer"></section>
    </>
  )
}

export default App
