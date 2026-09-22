import { ImageUploadForm } from './components/ImageUploadForm'
import './styles/areas.css'
import { ImageGallery } from './components/ImageGallery'


function App() {

  return (
    <>
    <div className="wrapper">
      <header className="header">˚*ੈ✩‧Last updated: September 2026✩˚</header>

      <div className="welcome">
        <h1 className="welcome-title">
          Welcome! &lt;3
        </h1>
        <span className='form-row'>
          <img
            className="basepicture"
            src="/images/vecteezy_woman-profile-mascot-vector-illustration-female-avatar-icon_9749878.jpg"
            alt="profile-avatar"
          />
          <p> Some introduction text... </p>
        </span>
      </div>

      <div className="blog">
        <p className="blogtitle">
            Updates
        </p>

        <ul className="updates-list">
          <li>✧ New pictures added to gallery</li>          
          <li>✧ More stuff coming soon...</li>
          <li>✧ Thanks for visiting &lt;3</li>
        </ul>
      </div>
      
      <main className='content'>
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
