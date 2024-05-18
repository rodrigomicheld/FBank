import 'bootstrap-icons/font/bootstrap-icons.css';
import styles from './bank.module.css'
import { Menulateral } from "../components/Menulateral";
import { BankHeader } from '../components/BankHeader';

export default function Layout({ children }) {
    return (  
        <>
            <div className={styles.bankHeaderContainer}>
                <BankHeader /> 
            </div>
            <div className={styles.bankContainer}>            
                <Menulateral />
                {children}
            </div>
        </> 
    )    
}