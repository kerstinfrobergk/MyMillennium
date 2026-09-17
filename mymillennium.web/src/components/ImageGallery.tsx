import { useState } from "react";
import type { ArtItem } from "../models/ArtItem";


export function ImageGallery(){
    const [selectedImage, setSelectedImage] = useState<ArtItem | null>(null);
    const [galleryItems, setGalleryItems] = useState<ArtItem[]>([]);

    return(
        <section
            style={{
                padding: '10px',
                minWidth: '200px',
                maxWidth: '200px',
                margin: '8px',
                background: '#78b8c6'
            }} >
            <div
                style={{
                    margin: '4px',
                    display: "grid",
                    gridTemplateColumns: "auto auto auto",
                    gap: "4px",
                    alignItems: "start"
                }}>
                // Three rectangles as image placeholders

            </div>

        </section>
    )
}