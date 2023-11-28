import {useState,useEffect} from 'react'
import { useParams } from 'react-router-dom'
import { useUserContext } from '../context/UserContext';
import { getResult } from '../services/ClientService';
import Result from '../components/Result';
import { Link } from 'react-router-dom';
export default function EventResultsPage(){
    const [results,setResults]=useState(null);
    const {id}=useParams();
    const {user}=useUserContext();

    useEffect(()=>{
        getResult(id,user.id)
        .then((res)=>{
           console.log(res)
           setResults(res);
        })
    },[]);
    return(<>
    
    <section className="py-5">
    <h1 className="headline">Results</h1>
            <div className="container px-4 px-lg-5 mt-5">
                <div className="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
                   
                      
                    {results===null?<h1>Loading...</h1>:results.map(r=><Result 
                    Name={r.giftName}
                    Count={r.count}
                    ImgUrl={r.imgUrl}
                    
                    key={r.giftName}/>)}
                   
                   
                </div>
               <div className="text-center"><Link className="btn btn-outline-dark mt-auto"to={`/trackVotingPage/${id}`} >Details </Link></div>
            </div>
        </section>
    </>)
}