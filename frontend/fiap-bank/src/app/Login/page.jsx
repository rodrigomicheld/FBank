'use client';
import useSWR from 'swr';
import styles from './login.module.css'
import Form from 'react-bootstrap/Form';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Button from 'react-bootstrap/Button';
import useAuthentication from '@/Hooks/useAuthentication';
import Link from 'next/link';

export default function Login(){
    const { formData,handleChange,getAuthentication } = useAuthentication();

    return (
        <div className={`${styles.containerLogin}`}>  
            <Container className={`justify-content-md-center ${styles.formLogin}`}>            
                <div id="divFormLogin">
                    <Row>
                        <Col md={{ span: 4, offset: 4 }}>
                            <Form.Label >Usuário</Form.Label>
                            <Form.Control
                                type="text"
                                name="username"
                                id="inputUsuario"   
                                value={formData.username}
                                onChange={handleChange}                
                            />
                        </Col>
                    </Row>
                    <Row>
                        <Col md={{ span: 4, offset: 4 }}>
                            <Form.Label >Password</Form.Label>
                            <Form.Control
                                type="password"
                                name="password"                                
                                aria-describedby="passwordHelpBlock"
                                value={formData.password}
                                onChange={handleChange}
                            />
                            <Form.Text id="passwordHelpBlock" muted >
                            </Form.Text>
                        </Col>
                    </Row>
                    <br />
                    <Row>
                        <Col md={{ span: 4, offset: 4 }}>
                            <Button variant="primary" type="submit" className='form-control'  onClick={()=>{getAuthentication(formData.username,formData.password)}} >
                                Acessar
                            </Button>
                        </Col>
                    </Row>
                    <Row>
                        <Col md={{ span: 4, offset: 4 }}>
                            <span>Não tem cadastro?</span>
                            <br/>
                            <Link href="/Cadastro">Cadastrar</Link>
                        </Col>
                    </Row>
                </div>
            </Container>
        </div>
      );
}