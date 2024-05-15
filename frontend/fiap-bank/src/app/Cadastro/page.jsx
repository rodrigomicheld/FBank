'use client';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import styles from './cadastro.module.css';
import useCadastro from '@/Hooks/useCadastro';

export default function Cadastro (){

    const { formData,handleChange,registrarCadastro } = useCadastro();

    
    return(<div className={styles.cadastroPanel}>
        <h1>Cadastro</h1>
        <br/>
        <br/>
        <Row>
            <Col md={{ span: 6, offset: 3 }}>
                <Form>
                    <Row >
                        <Form.Label>Nome</Form.Label>
                        <Form.Control type="text" placeholder="Insira o Nome" 
                        name='Nome'
                        value={formData.Nome}
                        onChange={handleChange}   />
                    </Row>
                    <Row >
                        <Form.Label>e-mail</Form.Label>
                        <Form.Control type="text" placeholder="Insira o CPF" 
                        name='Cpf'
                        value={formData.Cpf}
                        onChange={handleChange}/>
                    </Row>
                    <Row >
                        <Form.Label>CPF</Form.Label>
                        <Form.Control type="email" placeholder="Insira o  email"
                         name='Email'
                         value={formData.Email}
                         onChange={handleChange} />
                    </Row>
                    <Row>
                        <Form.Label>senha</Form.Label>
                        <Form.Control type="password" placeholder="Insira a Senha" 
                         name='Password'
                         value={formData.Password}
                         onChange={handleChange}
                         />                
                    </Row>
                    <Row >
                        <Col md={{ span: 4, offset: 8 }}> 
                            <Button variant="primary" size="ls" className={styles.btnCadastrar} onClick={()=>{ registrarCadastro(formData);}}>
                                Cadastrar                    
                            </Button>
                        </Col>
                    </Row>
                </Form>
            </Col>
        </Row>
    </div>)
}