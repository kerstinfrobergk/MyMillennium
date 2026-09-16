import { useState } from 'react'
import heroImg from './assets/hero.png'
import reactLogo from './assets/react.svg'
import viteLogo from './assets/vite.svg'
import React from 'react'
import './App.css'

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
