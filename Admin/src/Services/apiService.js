import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5015/api',
});

export const getData = (endpoint) => api.get(endpoint);


export const addEducation=(endpoint,data)=>api.post(endpoint,data);
export const updateEducation=(endpoint,data)=>api.put(endpoint,data);
export const getEducation = (endpoint) => api.get(endpoint);
export const deleteEducation=(endpoint)=>api.delete(endpoint)


export const postData = (endpoint, data) => api.post(endpoint, data);
export const updateData = (endpoint, data) => api.put(endpoint, data);
export const deleteData = (endpoint) => api.delete(endpoint);

export default api;
