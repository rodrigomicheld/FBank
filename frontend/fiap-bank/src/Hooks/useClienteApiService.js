'use client';

import useUtils from "./useUtils";

const useClienteApiService = ()=>{
    const url = 'http://10.0.0.109:8083/';
    const { getCookie } = useUtils();

    const sendPostRequest = async (endpoint,data, callback) =>{
        try {
            const cookie = getCookie("fiapBankCookie");
            const auth = `Bearer ${cookie}`
            
            const res = await fetch(url+endpoint,{
                method: "POST", // *GET, POST, PUT, DELETE, etc.
            // mode: "no-cors", // no-cors, *cors, same-origin
                headers: {
                'accept': '*/*',
                'Content-Type': 'application/json',
                'Authorization' : auth
                },
                body: JSON.stringify(data), // body data type must match "Content-Type" header
            });
            callback(res);           
        } catch (error) {
            console.error("Error:", error);
        }      
    }
    const sendGetRequest = async (endpoint,data, callback) =>{
        try {
            const cookie = getCookie("fiapBankCookie");
            const auth = `Bearer ${cookie}`
            const res = await fetch(url+endpoint,{
                method: "GET", // *GET, POST, PUT, DELETE, etc.
            // mode: "no-cors", // no-cors, *cors, same-origin
                headers: {
                'accept': '*/*',
                'Content-Type': 'application/json',
                'Authorization' : auth
                }
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
        sendPostRequest("api/fbank/Register",data, callback);
    }

    const getUserData = async (callback) =>{        
        sendGetRequest("api/fbank/Account/client",{}, callback);
    }

    //api/fbank/Account/extract?InitialDate=01%2F01%2F2000&FinalDate=01%2F01%2F2025&FlowType=1&_page=1&_size=100
    const getExtract = async(callback)=>{
        sendGetRequest("api/fbank/Account/extract",{}, callback);
    }
    const registerTransaction = (type, data, callback)=>{
        var url = type == 1 ? "api/fbank/Transaction/DepositAccount"
                : type == 2 ? "api/fbank/Transaction/WithDraw"  : "";
        if(type)
            sendPostRequest(url,data, callback);
    }
    return { registerUser, authenticar, getUserData,getExtract, registerTransaction}
}




export default useClienteApiService;