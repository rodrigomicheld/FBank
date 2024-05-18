'use client';
import useCliente from '@/Hooks/useCliente';
import styles from './bankHeader.module.css';
import Image from 'next/image';
import { useRouter } from "next/navigation";
import useUtils from '@/Hooks/useUtils';
import { useEffect } from 'react';
export const BankHeader = (props) =>{
    const router = useRouter();
    const { deleteCookie } = useUtils();
    const { userData,getCurrentUserData } = useCliente();
    const logOut = ()=>{
        deleteCookie("fiapBankCookie");
        router.push("../Login");
    }
    useEffect(()=>{
        getCurrentUserData();
    },[props?.userDataUpdated])
    return(
        <div className={styles.bankHeader}>
            <div className={styles.bankHeaderLogo}>
                <div>
                <Image 
                    src={'/fiapbanklogo.png'}
                    alt="Home"
                    height={80}
                    width={120}
                />
            </div>
            </div>
            <div className={styles.userPanelData}>
                <span >Olá,</span>
                <span className={styles.userPanelDataName}>{userData.name}</span>
            </div>
            <div className={styles.userPanelDataAccount}>
                <ul>
                    <li><span>Agência : {userData.agencyNumber}</span></li>
                    <li><span>Conta : {userData.accountNumber}</span></li>
                </ul>
            </div>
            <div className={styles.userPanelBalance}>
                <span>Saldo : R$ {userData.balance}</span>
            </div>

            <div className={styles.userSettingsPanel}>
                <div className={styles.userSettingsIcon}>
                    <i class="bi bi-person-circle"></i>
                    <button onClick={()=>{
                        logOut();
                    }}>
                        <i class="bi bi-box-arrow-in-left"></i>                  
                        Sair
                    </button>
                </div>
                {/* <div className={styles.userSettingsButttonLogout}>
                    
                </div> */}
            </div>
        </div>
    )
}