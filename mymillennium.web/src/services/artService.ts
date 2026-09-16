import { useState } from "react";

const baseUrl = "https://localhost:7164";
const uploadPath = "/api/Art/upload";

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