import {useState} from 'react'
import { logIn } from '../services/ClientService';
import { useUserContext } from '../context/UserContext';
import { Link ,useNavigate} from 'react-router-dom';
export default function Login(){
    const[loginFormData,setLoginFormData]=useState({username:"",password:""});
    const{userLogin}=useUserContext();
    const navigate=useNavigate();
    const handleSubmit=async(e)=>{
      e.preventDefault();
      await onSubmit();
    }
    const onSubmit=async()=>{
      logIn(loginFormData.username,loginFormData.password)
      .then((res)=>{
        console.log(res);
        userLogin(res)
      });
      navigate("/homePage");
    }

    const handleChange=(e)=>{
        const{name,value}=e.target
        setLoginFormData(prev=>({
            ...prev,
            [name]:value
        }));
    }
    return (<div className="login-container">
    <h1>Sign in to your account</h1>
    
    <form onSubmit={handleSubmit}  className="login-form">
   
        <input
            name="username"
            onChange={handleChange}
            type="text"
            placeholder="UserName"
            value={loginFormData.email}
        />
        <input
            name="password"
            onChange={handleChange}
            type="password"
            placeholder="Password"
            value={loginFormData.password}
        />
        <button>Log in</button>
        
    </form>
</div>)
}