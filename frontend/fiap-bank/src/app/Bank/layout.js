import 'bootstrap-icons/font/bootstrap-icons.css';
import styles from './bank.module.css'
import { Menulateral } from "../components/Menulateral";

export default function Layout({ children }) {
    return (<div className={styles.bankContainer}>                
        <Menulateral />
        {children}
    </div>)    
}