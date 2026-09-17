import type { ArtItem } from "../models/ArtItem";

const baseUrl = "https://localhost:7164";
const uploadPath = "/api/Art/upload";
const getPath = "/api/Art/getImages";

export async function uploadImage(
    file: File,
    title: string,
    description: string,
    itemCategory: number
){
    const formData = new FormData();

    formData.append("file", file);
    formData.append("title", title);
    formData.append("description", description);
    formData.append("itemCategory", itemCategory.toString());

    const response = await fetch(baseUrl + uploadPath, {
        method: "POST",
        body: formData
    });

    return response;
}

export async function getImages(): Promise<ArtItem[]> {
    const response = await fetch(baseUrl + getPath, {
        method: "GET"
    });

    const fetchedGalleryItems: ArtItem[] = await response.json();

    return fetchedGalleryItems;
}