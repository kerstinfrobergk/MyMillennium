import { ImageUploadForm } from './components/ImageUploadForm'
import './styles/areas.css'
import { ImageGallery } from './components/ImageGallery'


function App() {

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
    </>
  )
}
export default App
