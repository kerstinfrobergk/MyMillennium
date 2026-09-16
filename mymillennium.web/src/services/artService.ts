import { useState } from "react";


const baseUrl = "https://localhost:5080";
const uploadPath = "/api/Art/upload";

export async function uploadImage(
    imageFile: File,
    title: string,
    description: string,
    itemCategory: number
){
    const formData = new FormData();

    formData.append("imageFile", imageFile);
    formData.append("title", title);
    formData.append("description", description);
    formData.append("itemCategory", itemCategory.toString());

    const response = await fetch(baseUrl + uploadPath, {
        method: "POST",
        body: formData
    });

    return response;
}