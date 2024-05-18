'use client';
import { useEffect, useState } from "react";
import useClienteApiService from "./useClienteApiService";

const useExtract = ()=>{
    useEffect(()=>{
        getCurrentUserExtract();
   
    },[]);
    const [userExtract, setUserExtract] = useState([]);
    const  { getExtract} = useClienteApiService();

    const getCurrentUserExtract = ()=>{
        getExtract(async(response)=>{
            if(response.ok){
                const result = await response.json();
                console.log("Successo ao obter extrato do usuário");
                const extract = result.data.map( x=>{
                    return {...x,"description":x.description.split(" ")[1]}
                });
                setUserExtract(extract);  
            }else{
                return response.text().then((text) =>{    
                    console.log("Error ao obter dados usuário:", text);                 
                    setModalData({
                        textTitle:"Erro ao obter dados usuário",
                        textBody:text, 
                        show:true});

                });                
            }
        });
    }
    return {userExtract}
}
export default useExtract;