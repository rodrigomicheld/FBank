'use client';

import { useState } from "react";
import { useRouter } from "next/navigation";
import useUtils from "./useUtils";


const useAuthentication = ()=>{
    const router = useRouter();
    const { isNullOrEmpty, setCookie } = useUtils();
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
  
    
    const getAuthentication = (usuario, senha)=>{
        console.log(usuario, senha);
        //TODO > send request
        const cookie = "1234"
        setCookie("fiapBankCookie",cookie, 1);
        if(!isNullOrEmpty(cookie)){
            setTimeout(()=>{
                router.push('/Bank');
            },1000);
        }
    }

    return {formData,handleChange,getAuthentication}
}
export default useAuthentication;