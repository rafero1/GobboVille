using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Interface dos Nodes da BehaviourTree
public interface Node {
    NodeStatus run ();
}

/// Enum para facilitar entendimento do status das nodes
public enum NodeStatus {
    SUCCESS,
    FAIL,
    RUNNING
}

/// Composite Node pode ter vários filhos
public class CompositeNode : Node {

    List<Node> children;

    protected CompositeNode () {
        children = new List<Node> ();
    }

    public List<Node> getChildren () {
        return children;
    }

    public CompositeNode addChild (Node c) {
        children.Add (c);
        return this;
    }

    virtual public NodeStatus run () {
        throw new System.NotImplementedException ();
    }
}

/// Decorator Node só pode um
public class DecoratorNode : Node {
    Node child;

    public Node getChild () {
        return child;
    }

    public DecoratorNode setChild (Node c) {
        child = c;
        return this;
    }

    virtual public NodeStatus run () {
        throw new System.NotImplementedException ();
    }
}

/// Inverte o resultado do filho
public class Inverter : DecoratorNode {
    override public NodeStatus run () {
        NodeStatus result = getChild ().run ();
        if (result == NodeStatus.SUCCESS) return NodeStatus.FAIL;
        if (result == NodeStatus.FAIL) return NodeStatus.SUCCESS;
        else return result;
    }
}

/// Retorna success independente do resultado do filho
public class Succeder : DecoratorNode {
    override public NodeStatus run () {
        NodeStatus result = getChild ().run ();
        if (result == NodeStatus.SUCCESS || result == NodeStatus.FAIL) return NodeStatus.SUCCESS;
        else return result;
    }
}

/// Executa o filho até retornar fail
public class UntilFail : DecoratorNode {
    override public NodeStatus run () {
        while (getChild ().run () != NodeStatus.FAIL) {
            NodeStatus result = getChild ().run ();
            return NodeStatus.RUNNING;
        }
        return NodeStatus.SUCCESS;
    }
}

/// Executa todos os filhos em sequencia, enquanto eles retornarem success. Quando algum filho retornar fail, retorna false e para a execução.
public class Sequence : CompositeNode {
    private List<Node> done;

    public Sequence () {
        done = new List<Node> ();
    }

    override public NodeStatus run () {
        foreach (Node child in getChildren ()) {
            if (!done.Contains (child)) {
                NodeStatus result = child.run ();
                // Se o filho retornar success, passa pro próximo
                if (result == NodeStatus.SUCCESS) {
                    done.Add (child);
                    //return NodeStatus.RUNNING;
                } else if (result == NodeStatus.FAIL) {
                    // Se retornar fail, cancela execução e retorna fail
                    done.Clear ();
                    return NodeStatus.FAIL;
                } else {
                    return NodeStatus.RUNNING;
                }
            }
        }
        // Quando passar por todos os filhos sem problemas, retornar success
        done.Clear ();
        return NodeStatus.SUCCESS;
    }
}

/// É o contrário do Sequence. Quando algum filho retornar success, para a execução e retorna success.
public class Selector : CompositeNode {
    List<Node> done;

    public Selector () {
        done = new List<Node> ();
    }

    override public NodeStatus run () {
        foreach (Node child in getChildren ()) {
            if (!done.Contains (child)) {
                NodeStatus result = child.run ();
                // Se o filho retornar fail, passa pro próximo
                if (result == NodeStatus.FAIL) {
                    done.Add (child);
                    //return NodeStatus.RUNNING;
                } else if (result == NodeStatus.SUCCESS) {
                    // Se o filho retornar success, termina a execução e retorna success
                    done.Clear ();
                    return NodeStatus.SUCCESS;
                } else {
                    return NodeStatus.RUNNING;
                }
            }
        }
        // Quando passar por todos os filhos, e nenhum retornar success, retornar fail
        done.Clear ();
        return NodeStatus.FAIL;
    }
}

//////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                          //
//                                        Leaf nodes                                        //
//                                                                                          //
//////////////////////////////////////////////////////////////////////////////////////////////

/// Node para mover o BehaviourAgent para frente.
public class SimpleWalkNode : Node {
    public Transform transform;
    public float speed = 4f;

    public SimpleWalkNode (Transform t) {
        transform = t;
    }

    public NodeStatus run () {
        // Debug.Log("Running WalkNode!");
        transform.Translate ((transform.forward * speed) * Time.deltaTime);
        return NodeStatus.RUNNING;
    }
}

/// Node para mover o BehaviourAgent para algum alvo Transform.
public class WalkToNode : Node {
    Transform transform;
    Transform target;
    public float speed = 4f;

    public WalkToNode (Transform transform, Transform target) {
        this.transform = transform;
        this.target = target;
    }

    public NodeStatus run () {
        //Debug.Log("Running WalkToNode!");
        // Cancela se o target não existe
        if (target == null) {
            return NodeStatus.FAIL;
        }

        // Pega o vetor distância do transform até o alvo e normaliza pra obter valores pequenos para o movimento.
        Vector3 distance = target.position - transform.position;
        transform.Translate ((distance.normalized * speed) * Time.deltaTime);

        if (transform.position.x >= target.position.x - 1 &&
            transform.position.x <= target.position.x + 1 &&
            transform.position.z >= target.position.z - 1 &&
            transform.position.z <= target.position.z + 1) {
            return NodeStatus.SUCCESS;
        }

        return NodeStatus.RUNNING;
    }
}

/// Node para achar algum alvo aleatória.
public class FindTarget : Node {
    BehaviourAgent invader;
    string TargetType;

    public FindTarget (BehaviourAgent invader, string TargetType) {
        this.invader = invader;
        this.TargetType = TargetType;
    }

    public NodeStatus run () {
        List<GameObject> list = new List<GameObject> (GameObject.FindGameObjectsWithTag (TargetType));

        if (list.Count >= 1) {
            int joker = (int) Random.Range (0, list.Count - 1);
            invader.Target = list.ToArray () [joker].transform;

            if (invader.Target) {
                //Debug.Log ("FOUND TARGET: " + invader.Target.name);
                return NodeStatus.SUCCESS;
            } else {
                return NodeStatus.FAIL;
            }
        } else return NodeStatus.FAIL;
    }
}

/// Node para mover o BehaviourAgent para algum alvo BehaviourAgent.
public class WalkToTargetNode : Node {
    BehaviourAgent invader;
    Transform transform;
    public float speed = 4f;

    public WalkToTargetNode (BehaviourAgent invader) {
        this.invader = invader;
        this.transform = invader.transform;
    }

    public NodeStatus run () {
        Transform target = invader.Target;

        if (target == null) {
            return NodeStatus.FAIL;
        }

        // Pega o vetor distância do transform até o alvo e normaliza pra obter valores pequenos para o movimento.
        Vector3 distance = target.position - transform.position;
        transform.Translate ((distance.normalized * speed) * Time.deltaTime);

        if (transform.position.x >= target.position.x - 1 &&
            transform.position.x <= target.position.x + 1 &&
            transform.position.z >= target.position.z - 1 &&
            transform.position.z <= target.position.z + 1) {
            return NodeStatus.SUCCESS;
        }

        return NodeStatus.RUNNING;
    }
}

/// Node para encontrar uma posição aleatória.
class FindRandomPositionNode : Node {
    private GameObject floor;
    private BehaviourAgent BehaviourAgent;

    public FindRandomPositionNode (GameObject floor, BehaviourAgent BehaviourAgent) {
        this.floor = floor;
        this.BehaviourAgent = BehaviourAgent;
    }

    public NodeStatus run () {
        Vector3 position = new Vector3 (Random.Range (-50, 50), 0f, Random.Range (-48, 30));
        BehaviourAgent.targetPosition = position;
        return NodeStatus.SUCCESS;
    }
}

/// Node para mover o BehaviourAgent para alguma posição Vector3.
public class WalkToPositionNode : Node {
    private BehaviourAgent BehaviourAgent;

    public WalkToPositionNode (BehaviourAgent BehaviourAgent) {
        this.BehaviourAgent = BehaviourAgent;
    }

    public NodeStatus run () {
        //Debug.Log ("Walking", BehaviourAgent);
        Vector3 target = BehaviourAgent.targetPosition;
        Transform transform = BehaviourAgent.transform;
        if (target == null) {
            return NodeStatus.FAIL;
        }

        // Pega o vetor distância do transform até o alvo e normaliza pra obter valores pequenos para o movimento.
        Vector3 distance = target - transform.position;
        transform.Translate ((distance.normalized * 4f) * Time.deltaTime);

        if (transform.position.x >= target.x - 1 &&
            transform.position.x <= target.x + 1 &&
            transform.position.z >= target.z - 1 &&
            transform.position.z <= target.z + 1) {
            return NodeStatus.SUCCESS;
        }

        return NodeStatus.RUNNING;
    }
}

/// Node para esperar por uma determinada quantidade de tempo.
public class WaitNode : Node {
    private BehaviourAgent BehaviourAgent;
    private float time;
    private float stopTime;
    private float minSeconds;
    private float maxSeconds;

    public WaitNode (float seconds, BehaviourAgent BehaviourAgent) {
        this.BehaviourAgent = BehaviourAgent;
        this.minSeconds = seconds;
        this.maxSeconds = seconds;
        this.stopTime = seconds;
        time = 0;
    }

    public WaitNode (float minSeconds, float maxSeconds, BehaviourAgent BehaviourAgent) {
        this.BehaviourAgent = BehaviourAgent;
        this.minSeconds = minSeconds;
        this.maxSeconds = maxSeconds;
        this.stopTime = maxSeconds;
        time = 0;
    }

    public NodeStatus run () {
        // Selecionar ponto de parada
        if (minSeconds != maxSeconds && time <= 0) {
            stopTime = (int) Random.Range (minSeconds, maxSeconds);
        }

        // Contando os segundos
        time += 1 * Time.deltaTime;
        //Debug.Log ($"Waiting ({(int)time} out of {stopTime})", BehaviourAgent);
        if (time >= stopTime) {
            time = 0;
            stopTime = 0;
            return NodeStatus.SUCCESS;
        } else {
            return NodeStatus.RUNNING;
        }
    }
}