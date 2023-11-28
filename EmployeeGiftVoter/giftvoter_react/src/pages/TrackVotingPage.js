import {useState,useEffect} from 'react'
import { useUserContext } from '../context/UserContext'
import { useParams } from 'react-router-dom'
import {trackVoting}from '../services/ClientService'
import VoteRecord from '../components/VoteRecord'
export default function TrackVotingPage(){
    const [details,setDetails]=useState(null);
    const {user}=useUserContext();
    const {id}=useParams();

    useEffect(()=>{
       trackVoting(id,user.id)
       .then((res)=>{
         console.log(res);
         setDetails(res);
       });
    },[]);
    return(<>
     
     <section className="py-5">
     <h1 className="headline">Voting History</h1>
            <div className="container px-4 px-lg-5 mt-5">
            
            
                <div className="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
                   
               
                    {details===null?<h1>Loading...</h1>:details.map(d=><VoteRecord 
                    VoterName={d.voterName}
                    GiftVoted={d.giftVoted}
                    ImgUrl={d.imgUrl}
                    
                    key={d.giftVoted}/>)}
                   
                   
                </div>
            </div>
        </section>
    </>)
}