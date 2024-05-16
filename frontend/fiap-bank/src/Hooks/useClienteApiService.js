'use client';
const useClienteApiService = ()=>{
    const url = 'http://10.0.0.109:8083/';

    const sendRequest = async (endpoint, data, callback) =>{
        try {
            const res = await fetch(url+endpoint,{
                method: "POST", // *GET, POST, PUT, DELETE, etc.
            // mode: "no-cors", // no-cors, *cors, same-origin
                headers: {
                'accept': '*/*',
                'Content-Type': 'application/json'
                },
                body: JSON.stringify(data), // body data type must match "Content-Type" header
            });
            callback(res);           
        } catch (error) {
            console.error("Error:", error);
        }      
    }
    const authenticar = async (login, senha, callback) =>{
        //api/fbank/Register'
        try {
            const res = await fetch(url+`api/fbank/Login?Document=${login}&Password=${senha}`,{
                method: "GET", // *GET, POST, PUT, DELETE, etc.
            // mode: "no-cors", // no-cors, *cors, same-origin
                headers: {
                'accept': '*/*',
                'Content-Type': 'application/json'
                }                
            });
            callback(res);           
        } catch (error) {
            console.error("Error:", error);
        }      
    }
    const registerUser = async (data, callback) =>{        
        sendRequest("api/fbank/Register",data, callback);
    }

    return { registerUser, authenticar}
}




export default useClienteApiService;