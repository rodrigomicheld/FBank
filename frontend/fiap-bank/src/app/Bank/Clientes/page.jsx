
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import InputGroup from 'react-bootstrap/InputGroup';
import Table from 'react-bootstrap/Table';
import styles from './cliente.module.css';

export default function Clientes() {

    const clientesData = [{
        Nome:"Mateus",
        NumeroConta:"1324",
        Saldo:"0",

    }]
    return(<div className={styles.clienteComponent}>
            <InputGroup className="mb-3">
                <Form.Control
                    placeholder="Digite o cliente para pesquisar"                    
                    aria-describedby="basic-addon2"
                />
                <Button variant="outline-secondary" id="button-addon2">
                    Pesquisar
                </Button>
            </InputGroup>
            <Table striped bordered hover size="lg">
                <thead>
                    <tr>
                    <th>#</th>
                    <th>Nome</th>
                    <th>Conta</th>
                    <th>Saldo</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td></td>
                        <td>{clientesData[0].Nome}</td>
                        <td>{clientesData[0].NumeroConta}</td>
                        <td>{clientesData[0].Saldo}</td>
                    </tr>
                </tbody>
                </Table>
    </div>)
}