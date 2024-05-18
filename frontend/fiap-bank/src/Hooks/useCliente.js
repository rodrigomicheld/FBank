'use client';
import { useEffect, useState } from "react";
import useClienteApiService from "./useClienteApiService";

const useCliente = ()=>{
    const initialUserData = {
        name:'',
        cpf:'',
        accountNumber:'',
        agencyNumber:'',
        balance:0
    }    
    useEffect(()=>{
        getCurrentUserData();
   
    },[]);
    const [userData, setUserData] = useState(initialUserData);    
    const  { getUserData ,getExtract} = useClienteApiService();
   
    const getCurrentUserData = ()=>{
        console.info("Buscando dados usuário");
        getUserData(async(response)=>{
            if(response.ok){
                const result = await response.json();
                console.log("Successo ao obter dados usuário");
                setUserData({
                    name:result.name,
                    cpf:result.document,
                    accountNumber:result.accounts[0].number,
                    agencyNumber:result.accounts[0].agency.number,
                    balance:result.accounts[0].balance,
                });  
            }else{
                return response.text().then((text) =>{    
                    console.log("Error ao obter dados do  usuário:", text);                 
                    setModalData({
                        textTitle:"Erro ao obter extrato usuário",
                        textBody:text, 
                        show:true});

                });                
            }
        });
    }

    return {userData,getCurrentUserData}
}
export default useCliente;