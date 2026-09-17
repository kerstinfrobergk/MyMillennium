import { useEffect, useState } from "react";
import type { ArtItem } from "../models/ArtItem";
import { getImages } from "../services/artService";


export function ImageGallery(){
    const [selectedImage, setSelectedImage] = useState<ArtItem | null>(null);
    const [galleryItems, setGalleryItems] = useState<ArtItem[]>([]);

    useEffect(() =>{
        async function loadImages() {
            const images = await getImages();
            setGalleryItems(images);
        }

        loadImages();
    }, []);

    return(
        <section
            style={{
                padding: '10px',
                minWidth: '400px',
                maxWidth: '500px',
                minHeight: '200px',
                margin: '8px auto',
                background: '#78b8c6',
                border: "4px solid #9d8e09",                
            }} >
            <div>
                <h2 style={{ color: '#063b4b' }}>Gallery</h2>
            </div>
            <div
                style={{
                    margin: '10px',
                    display: "flex",
                    flexWrap: "wrap",
                    gap: "16px",
                }}>
                {galleryItems.map((item) => (
                    <img
                        key={item.imageUrl}
                        src={item.imageUrl}
                        alt={item.title}
                        title={item.title}
                        onClick={() => setSelectedImage(item)}
                        style={{
                            width: "80px",
                            aspectRatio: "1/1",
                            objectFit: "cover",
                            cursor: "pointer",
                            marginBottom: "6px",
                            border: "2px solid"
                        }}
                    />
                ))}
            </div>

            {selectedImage && (
                <div style={{ marginTop: "10px" }}>
                    <img
                        style={{ width: "100%", display: "block" }}
                        src={selectedImage.imageUrl}
                        alt={selectedImage.title}
                    />

                    <strong>{selectedImage.title}</strong>
                    <p>{selectedImage.description}</p>
                </div>
            )}

        </section>
    )
}