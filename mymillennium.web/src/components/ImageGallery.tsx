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
            <div className="gallery-thumbnails" >
                {galleryItems.map((item) => (
                    <div key={item.imageUrl}
                        className="thumbnail-container"
                    >
                        {item.thumbnailUrl ? (
                            <img
                                src={item.thumbnailUrl}
                                alt={item.title ?? ""}
                                title={item.title ?? ""}
                                className="thumbnail-image"
                                onClick={() => setSelectedImage(item)}
                            />
                        ) : (
                            <div className="thumbnail-placeholder" >
                                processing...
                            </div>
                        )}
                    </div>
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