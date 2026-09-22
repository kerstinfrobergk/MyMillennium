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
        <section className="image-gallery" >
            <div>
                <h2 className="gallery-heading">Gallery</h2>
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
                <div className="selected-image" >
                    <img
                        src={selectedImage.imageUrl}
                        alt={selectedImage.title ?? ""}
                    />

                    <strong>{selectedImage.title}</strong>
                    <p>{selectedImage.description}</p>
                </div>
            )}
        </section>
    )
}