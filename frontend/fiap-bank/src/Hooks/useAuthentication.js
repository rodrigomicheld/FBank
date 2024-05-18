'use client';

import { useState } from "react";
import { useRouter } from "next/navigation";
import useUtils from "./useUtils";
import useClienteApiService from "./useClienteApiService";


const useAuthentication = ()=>{
    const router = useRouter();
    const { isNullOrEmpty, setCookie } = useUtils();
    const { authenticar} = useClienteApiService();
    const [formData, setFormData] = useState({
        username: '',        
        password: '',
    });
    
    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prevState => ({
            ...prevState,
            [name]: value,
        }));
    };
  
    
    const getAuthentication = async (usuario, senha)=>{
        console.log(usuario, senha);
        try {            
            authenticar(usuario, senha,  async (response)=>{
                if(response.ok){
                    const result = await response.json();
                    console.log("Success:", result);     
                    const token  = result?.token;              
                    if(!isNullOrEmpty(token)){
                        setCookie("fiapBankCookie",token, 1);
                        setTimeout(()=>{
                            router.push('/Bank');
                        },1000);
                    }
                }else{
                    return response.text().then((text) =>{ 
                        throw new Error(text);
                    });                
                }
            });
        } catch (error) {
            console.error("Error:", error);
        }      
        
    }

    return {formData,handleChange,getAuthentication}
}
export default useAuthentication;